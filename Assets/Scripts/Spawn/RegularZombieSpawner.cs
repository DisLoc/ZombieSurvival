using UnityEngine;

public sealed class RegularZombieSpawner : EnemySpawner, IBossEventHandler, IBossEventEndedHandler
{
    private bool _onBossEvent;

    private BreakpointList<EnemyBreakpoint> _breakpoints;  

    protected override void OnEnable()
    {
        base.OnEnable();

        _breakpoints = new BreakpointList<EnemyBreakpoint>(_levelContext.EnemyBreakpoints);
    }

    public override void OnUpdate()
    {
        if (!_onBossEvent)
        {
            Spawn(GetSpawnPosition());
            base.OnUpdate();
        }
        else return;
    }

    public override void OnFixedUpdate()
    {
        if (!_onBossEvent)
        {
            base.OnFixedUpdate();
        }
        else return;
    }

    public override void OnLevelProgressUpdate(int progress)
    {
        Breakpoint breakpoint = _breakpoints.CheckReaching(progress);

        if (breakpoint != null)
        {
            if (_isDebug) Debug.Log("Enemy strike!");

            _combiner = new ChanceCombiner<Enemy>((breakpoint as EnemyBreakpoint).SpawningEnemies);
            _maxUnitsOnScene = (breakpoint as EnemyBreakpoint).MaxUnitsOnScene;

            DispelUpgrades();
            ReplacePools();

            foreach (ObjectChanceSpawn<Enemy> spawnChance in (breakpoint as EnemyBreakpoint).SpawningEnemies)
            {
                _spawners.Add(new ObjectSpawner<Enemy>(spawnChance.Object, _maxUnitsOnScene, transform));
            }

            GetUpgrade();
        }

        base.OnLevelProgressUpdate(progress);
    }

    public void OnBossEvent()
    {
        if (_spawners != null)
        {
            foreach (var spawner in _spawners)
            {
                if (spawner.SpawnCount == 0) continue;

                for (int i = 0; i < spawner.SpawnCount; i++)
                {
                    if (spawner.SpawnedObjects[i] != null)
                    {
                        spawner.SpawnedObjects[i].Die();
                    }
                }

                spawner.SpawnedObjects.Cleanup();
            }
        }

        if (_prevSpawners != null)
        {
            foreach (var spawner in _prevSpawners)
            {
                if (spawner.SpawnCount == 0) continue;

                for (int i = 0; i < spawner.SpawnCount; i++)
                {
                    if (spawner.SpawnedObjects[i] != null)
                    {
                        spawner.SpawnedObjects[i].Die();
                    }
                }

                spawner.ClearPool();
            }

            _prevSpawners.Clear();
        }

        _onBossEvent = true;
    }

    public void OnBossEventEnd()
    {
        _onBossEvent = false;
    }
}
