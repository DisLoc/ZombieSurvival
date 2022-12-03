using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnHerd : EnemySpawner, IBossEventHandler
{

    private BreakpointList<HerdBreakpoint> _breakpoints;


 

    private bool _onBossEvent;

    protected override void OnEnable()
    {
        base.OnEnable();

        _breakpoints = new BreakpointList<HerdBreakpoint>(_levelContext.HerdBreakpoints);
    }

    public override void OnUpdate()
    {
        if (!_onBossEvent && CurrentSpawned > 0)
        {
            base.OnUpdate();
            TryClearPool();
        }
        else return;
    }

    protected Vector3 GetDeltaPos()
    {
        return new Vector3
            (
                Random.Range(0, 2) > 0 ? _spawnDeltaDistance : -_spawnDeltaDistance,
                0f,
                Random.Range(0, 2) > 0 ? _spawnDeltaDistance : -_spawnDeltaDistance
            );
    }
    public override void OnFixedUpdate()
    {
        if (!_onBossEvent && CurrentSpawned > 0)
        {
            base.OnFixedUpdate();
            TryClearPool();
        }
        else return;
    }

    public override void OnLevelProgressUpdate(int progress)
    {
        Breakpoint breakpoint = _breakpoints.CheckReaching(progress);

        if (breakpoint != null)
        {
            if (_isDebug) Debug.Log("Horde incoming!");

            _maxUnitsOnScene = (breakpoint as HerdBreakpoint).SpawnCount;

            DispelUpgrades();
            ReplacePools();

            _spawners.Add(new ObjectSpawner<Enemy>((breakpoint as HerdBreakpoint).EnemyToSpawnPrefab, _maxUnitsOnScene, transform));

            Vector3 spawnPos = GetSpawnPosition();
            for (int i = 0; i < (breakpoint as HerdBreakpoint).SpawnCount; i++)
            {
                Spawn(spawnPos);
            }

            GetUpgrade();
        }

        base.OnLevelProgressUpdate(progress);
    }

    protected override void Spawn(Vector3 position)
    {
        if (_spawners != null && _spawners.Count > 0 && position != -Vector3.one)
        {
            Enemy spawnedEnemy = _spawners[0].Spawn(new Vector3
                    (
                        position.x,
                        _levelContext.LevelBuilder.GridHeight + _spawners[0].Prefab.Collider.height * _spawners[0].Prefab.transform.localScale.y * 0.5f,
                        position.z
                    ) + new Vector3(Random.Range(0, 4), 0, Random.Range(0, 4)));

            spawnedEnemy.Initialize(_player, _spawners[0]);

            _totalSpawned++;
        }
        else return;
    }

    protected override Vector3 GetSpawnPosition()
    {
        return _player.transform.position + GetDeltaPos();
    }

    public void OnBossEvent()
    {
        if (_spawners != null)
        {
            foreach (var spawner in _spawners)
            {
                for (int i = 0; i < spawner.SpawnCount; i++)
                {
                    spawner.SpawnedObjects[i]?.Die();
                }
            }
        }

        if (_prevSpawners != null)
        {
            foreach (var spawner in _prevSpawners)
            {
                for (int i = 0; i < spawner.SpawnCount; i++)
                {
                    spawner.SpawnedObjects[i]?.Die();
                }
            }
        }

        ClearPools();
    }

    public void OnBossEventEnd()
    {
        _onBossEvent = false;
    }

    private void TryClearPool()
    {
        if (CurrentSpawned == 0)
        {
            foreach (Enemy zombie in _spawners[0].SpawnedObjects.List)
            {
                zombie.Die();
            }

            foreach (var pool in _spawners)
            {
                foreach (Enemy zombie in pool.SpawnedObjects.List)
                {
                    zombie.Die();
                }
            }

            ClearPools();
        }
    }

    protected override Vector3 GetMoveDirection(Vector3 playerPos, Vector3 enemyPos)
    {
        return base.GetMoveDirection(playerPos, enemyPos);
    }
}
