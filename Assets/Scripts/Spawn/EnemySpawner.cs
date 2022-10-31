using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static UnityEngine.Mathf;

public class EnemySpawner : Spawner, IUpdatable, IFixedUpdatable, IBossEventHandler, IBossEventEndedHandler
{
    protected int _maxUnitsOnScene;
    protected int _totalSpawned;

    private bool _onBossEvent;

    private BreakpointList<EnemyBreakpoint> _breakpoints;

    private List<ObjectSpawner<Zombie>> _spawners;
    private ChanceCombiner<Zombie> _combiner;

    #region Inject
    [Inject] private Player _player;
    [Inject] private LevelContext _levelContext;
    #endregion

    protected override void OnEnable()
    {
        base.OnEnable();

        _breakpoints = _levelContext.EnemyBreakpoints;
        _spawners = new List<ObjectSpawner<Zombie>>();
    }

    public override void OnUpdate()
    {
        if (_onBossEvent || _spawners == null || _spawners.Count == 0) return;

        Spawn(GetSpawnPosition());

        foreach (var spawner in _spawners)
        {
            if (spawner.SpawnCount == 0) continue;

            for (int i = 0; i < spawner.SpawnCount; i++)
            { 
                spawner.SpawnedObjects[i]?.OnUpdate();
            }
            spawner.SpawnedObjects.Cleanup();
        }
    }

    public override void OnFixedUpdate()
    {
        if (_onBossEvent || _spawners == null || _spawners.Count == 0) return;

        foreach (var spawner in _spawners)
        {
            if (spawner.SpawnCount == 0) continue;

            for (int i = 0; i < spawner.SpawnCount; i++)
            {
                spawner.SpawnedObjects[i]?.OnFixedUpdate();
            }
            spawner.SpawnedObjects.Cleanup();
        }
    }

    public override void OnLevelProgressUpdate(int progress)
    {
        Breakpoint breakpoint = _breakpoints.CheckReaching(progress);

        if (breakpoint != null)
        {
            if (_isDebug) Debug.Log("Enemy strike!");

            _combiner = new ChanceCombiner<Zombie>((breakpoint as EnemyBreakpoint).SpawningEnemies);
            _maxUnitsOnScene = (breakpoint as EnemyBreakpoint).MaxUnitsOnScene;

            ClearPools();

            foreach (ObjectChanceSpawn<Zombie> spawnChance in (breakpoint as EnemyBreakpoint).SpawningEnemies)
            {
                _spawners.Add(new ObjectSpawner<Zombie>(spawnChance.Object, _maxUnitsOnScene, transform));
            }
        }
    }

    public void OnBossEvent()
    {
        if (_spawners == null || _spawners.Count == 0) return;

        foreach (var spawner in _spawners)
        {
            if (spawner.SpawnCount == 0) continue;

            for (int i = 0; i < spawner.SpawnCount; i++)
            {
                if(spawner.SpawnedObjects[i] != null)
                {
                    spawner.Release(spawner.SpawnedObjects[i]);
                }
            }

            spawner.SpawnedObjects.Cleanup();
        }

        _onBossEvent = true;
    }

    public void OnBossEventEnd()
    {
        _onBossEvent = false;
    }

    protected override void Spawn(Vector3 position)
    {
        Zombie enemy = _combiner.GetStrikedObject();

        if (enemy == null || _spawners == null) return;

        int unitsOnScene = 0;

        foreach(ObjectSpawner<Zombie> pool in _spawners)
        {
            unitsOnScene += pool.SpawnCount;
        }

        if (unitsOnScene >= _maxUnitsOnScene)
        {
            return;
        }

        ObjectSpawner<Zombie> spawner = null;

        foreach (ObjectSpawner<Zombie> pool in _spawners)
        {
            if (pool.Prefab.Equals(enemy))
            {
                spawner = pool;
                break;
            }
        }

        if (spawner == null) return;

        Zombie spawnedEnemy = spawner.Spawn(position);

        if (spawnedEnemy != null)
        {
            spawnedEnemy.Initialize(_player, spawner);
            _totalSpawned++;
        }
    }

    private Vector3 GetSpawnPosition()
    {
        Vector3 playerPos = _player.transform.position;
        int unitsOnScene = 0;

        foreach (ObjectSpawner<Zombie> pool in _spawners)
        {
            unitsOnScene += pool.SpawnCount;
        }

        return new Vector3
            (
                Cos(_totalSpawned % _maxUnitsOnScene) * _spawnDeltaDistance + playerPos.x, 
                playerPos.y, 
                Sin(_totalSpawned % _maxUnitsOnScene) * _spawnDeltaDistance + playerPos.z
            );
    }

    private void ClearPools()
    {
        if (_spawners != null)
        {
            foreach(ObjectSpawner<Zombie> pool in _spawners)
            {
                pool.ClearPool();
            }

            _spawners.Clear();
        }
    }
}
