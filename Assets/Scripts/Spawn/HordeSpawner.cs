using UnityEngine;

public sealed class HordeSpawner : EnemySpawner, IBossEventHandler
{
    private BreakpointList<HordeBreakpoint> _breakpoints;

    private bool _onBossEvent;

    protected override void OnEnable()
    {
        base.OnEnable();

        _breakpoints = new BreakpointList<HordeBreakpoint>(_levelContext.HordeBreakpoints);
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

            _maxUnitsOnScene = (breakpoint as HordeBreakpoint).SpawnCount;

            DispelUpgrades();
            ReplacePools();

            _spawners.Add(new ObjectSpawner<Enemy>((breakpoint as HordeBreakpoint).EnemyToSpawnPrefab, _maxUnitsOnScene, transform));

            for (int i = 0; i < (breakpoint as HordeBreakpoint).SpawnCount; i++)
            {
                Spawn(GetSpawnPosition());
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
                    ));

            spawnedEnemy.Initialize(_player, _spawners[0]);

            _totalSpawned++;
        }
        else return;
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
            ClearPools();
        }
    }
}
