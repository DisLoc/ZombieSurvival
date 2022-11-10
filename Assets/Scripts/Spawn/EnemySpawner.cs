using System.Collections.Generic;
using UnityEngine;
using Zenject;

using static UnityEngine.Mathf;

public sealed class EnemySpawner : Spawner, IBossEventHandler, IBossEventEndedHandler
{
    private int _maxUnitsOnScene;
    private int _totalSpawned;

    private bool _onBossEvent;

    private BreakpointList<EnemyBreakpoint> _breakpoints;
    private BreakpointList<UpgradeBreakpoint> _upgradeBreakpoints;

    private Upgrade _currentUpgrade;

    private List<ObjectSpawner<Enemy>> _prevSpawners;
    private List<ObjectSpawner<Enemy>> _spawners;
    private ChanceCombiner<Enemy> _combiner;

    #region Inject
    [Inject] private Player _player;
    [Inject] private LevelContext _levelContext;
    #endregion

    protected override void OnEnable()
    {
        base.OnEnable();

        _breakpoints = _levelContext.EnemyBreakpoints;
        _upgradeBreakpoints = _levelContext.EnemyUpgradeBreakpoints;

        _spawners = new List<ObjectSpawner<Enemy>>();
        _prevSpawners = new List<ObjectSpawner<Enemy>>();
    }

    public override void OnUpdate()
    {
        if (_onBossEvent || _spawners == null) return;

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

        foreach (var spawner in _prevSpawners)
        {
            if (spawner.SpawnCount == 0) continue;

            for (int i = 0; i < spawner.SpawnCount; i++)
            {
                spawner.SpawnedObjects[i]?.OnUpdate();
            }

            spawner.SpawnedObjects.Cleanup();

            if (spawner.SpawnCount == 0)
            {
                spawner.ClearPool();
            }
        }

        _prevSpawners.RemoveAll(item => item.SpawnedObjects == null);
    }

    public override void OnFixedUpdate()
    {
        if (_onBossEvent || _spawners == null) return;

        foreach (var spawner in _spawners)
        {
            if (spawner.SpawnCount == 0) continue;

            for (int i = 0; i < spawner.SpawnCount; i++)
            {
                spawner.SpawnedObjects[i]?.OnFixedUpdate();
            }
            
            spawner.SpawnedObjects.Cleanup();
        }

        foreach (var spawner in _prevSpawners)
        {
            if (spawner.SpawnCount == 0) continue;

            for (int i = 0; i < spawner.SpawnCount; i++)
            {
                spawner.SpawnedObjects[i]?.OnFixedUpdate();
            }

            spawner.SpawnedObjects.Cleanup();

            if (spawner.SpawnCount == 0)
            {
                spawner.ClearPool();
            }
        }

        _prevSpawners.RemoveAll(item => item.SpawnedObjects == null);
    }

    public override void OnLevelProgressUpdate(int progress)
    {
        Breakpoint breakpoint = _breakpoints.CheckReaching(progress);

        if (breakpoint != null)
        {
            if (_isDebug) Debug.Log("Enemy strike!");

            _combiner = new ChanceCombiner<Enemy>((breakpoint as EnemyBreakpoint).SpawningEnemies);
            _maxUnitsOnScene = (breakpoint as EnemyBreakpoint).MaxUnitsOnScene;

            ClearPools();

            foreach (ObjectChanceSpawn<Enemy> spawnChance in (breakpoint as EnemyBreakpoint).SpawningEnemies)
            {
                _spawners.Add(new ObjectSpawner<Enemy>(spawnChance.Object, _maxUnitsOnScene, transform));
            }

            DispelUpgrades();
            GetUpgrade();
        }

        Breakpoint upgradeBreakpoint = _upgradeBreakpoints.CheckReaching(progress);

        if (upgradeBreakpoint != null)
        {
            if (_isDebug) Debug.Log("Enemy upgrade!");

            DispelUpgrades();

            _currentUpgrade = (upgradeBreakpoint as UpgradeBreakpoint).Upgrade;

            GetUpgrade();
        }
    }

    public void OnBossEvent()
    {
        if ((_spawners == null || _spawners.Count == 0) && (_prevSpawners == null || _prevSpawners.Count == 0)) return;

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

        foreach (var spawner in _prevSpawners)
        {
            spawner.ClearPool();
        }

        _prevSpawners.Clear();

        _onBossEvent = true;
    }

    public void OnBossEventEnd()
    {
        _onBossEvent = false;
    }

    protected override void Spawn(Vector3 position)
    {
        Enemy enemy = _combiner.GetStrikedObject();

        if (enemy == null || _spawners == null) return;

        int unitsOnScene = 0;

        foreach(ObjectSpawner<Enemy> pool in _spawners)
        {
            unitsOnScene += pool.SpawnCount;
        }

        if (unitsOnScene >= _maxUnitsOnScene)
        {
            return;
        }

        ObjectSpawner<Enemy> spawner = null;

        foreach (ObjectSpawner<Enemy> pool in _spawners)
        {
            if (pool.Prefab.Equals(enemy))
            {
                spawner = pool;
                break;
            }
        }

        if (spawner == null) return;

        Enemy spawnedEnemy = spawner.Spawn(position);

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

        foreach (ObjectSpawner<Enemy> pool in _spawners)
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

    /// <summary>
    /// Removes current pools to prevPools (it will be destroyed when all enemies return to pool)
    /// </summary>
    private void ClearPools()
    {
        if (_spawners != null)
        {
            foreach(ObjectSpawner<Enemy> pool in _spawners)
            {
                if (pool.SpawnCount == 0)
                {
                    pool.ClearPool();
                }
                else
                {
                    _prevSpawners.Add(pool);
                }
            }

            _spawners.Clear();
        }
    }

    /// <summary>
    /// Dispel upgrade from enemies (spawned and enemies in pool)
    /// </summary>
    private void DispelUpgrades()
    {
        if (_currentUpgrade == null)
        {
            if (_isDebug) Debug.Log("Current upgrade is null!");

            return;
        }

        if (_spawners != null)
        {
            foreach (ObjectSpawner<Enemy> pool in _spawners)
            {
                foreach(Enemy zombie in pool.Objects)
                {
                    zombie?.DispelUpgrade(_currentUpgrade);
                }

                foreach(Enemy zombie in pool.SpawnedObjects.List)
                {
                    zombie?.DispelUpgrade(_currentUpgrade);
                }
            }
        }

        if (_prevSpawners != null)
        {
            foreach (ObjectSpawner<Enemy> pool in _prevSpawners)
            {
                foreach (Enemy zombie in pool.Objects)
                {
                    zombie?.DispelUpgrade(_currentUpgrade);
                }

                foreach (Enemy zombie in pool.SpawnedObjects.List)
                {
                    zombie?.DispelUpgrade(_currentUpgrade);
                }
            }
        }
    }

    /// <summary>
    /// Add upgrade to enemies (spawned and enemies in pool)
    /// </summary>
    private void GetUpgrade()
    {
        if (_currentUpgrade == null)
        {
            if (_isDebug) Debug.Log("Try get null upgrade!");

            return;
        }

        if (_spawners != null)
        {
            foreach (ObjectSpawner<Enemy> pool in _spawners)
            {
                foreach (Enemy zombie in pool.Objects)
                {
                    zombie?.GetUpgrade(_currentUpgrade);
                }

                foreach (Enemy zombie in pool.SpawnedObjects.List)
                {
                    zombie?.GetUpgrade(_currentUpgrade);
                }
            }
        }

        if (_prevSpawners != null)
        {
            foreach (ObjectSpawner<Enemy> pool in _prevSpawners)
            {
                foreach (Enemy zombie in pool.Objects)
                {
                    zombie?.GetUpgrade(_currentUpgrade);
                }

                foreach (Enemy zombie in pool.SpawnedObjects.List)
                {
                    zombie?.GetUpgrade(_currentUpgrade);
                }
            }
        }
    }
}
