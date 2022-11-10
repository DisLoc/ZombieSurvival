using UnityEngine;
using Zenject;
using static UnityEngine.Mathf;

public sealed class HordeSpawner : Spawner, IBossEventHandler, IBossEventEndedHandler
{
    private ObjectSpawner<Enemy> _spawner;
    private BreakpointList<HordeBreakpoint> _breakpoints;

    private BreakpointList<UpgradeBreakpoint> _upgradeBreakpoints;

    private Upgrade _currentUpgrade;

    private int _spawned;
    private bool _onBossEvent;

    [Inject] private Player _player;
    [Inject] private LevelContext _levelContext;


    protected override void OnEnable()
    {
        base.OnEnable();

        _breakpoints = _levelContext.HordeBreakpoints;
        _upgradeBreakpoints = _levelContext.EnemyUpgradeBreakpoints;
    }

    public override void OnUpdate()
    {
        if (_onBossEvent || _spawner == null || _spawner.SpawnCount == 0) return;

        for (int i = 0; i < _spawner.SpawnCount; i++)
        {
            _spawner.SpawnedObjects[i]?.OnUpdate();
        }
        _spawner.SpawnedObjects.Cleanup();
    }

    public override void OnFixedUpdate()
    {
        if (_onBossEvent || _spawner == null || _spawner.SpawnCount == 0) return;

        for (int i = 0; i < _spawner.SpawnCount; i++)
        {
            _spawner.SpawnedObjects[i]?.OnFixedUpdate();
        }
        _spawner.SpawnedObjects.Cleanup();
    }

    public override void OnLevelProgressUpdate(int progress)
    {
        Breakpoint breakpoint = _breakpoints.CheckReaching(progress);

        if (breakpoint != null)
        {
            if (_isDebug) Debug.Log("Horde incoming!");

            _spawned = 0;

            if (_spawner != null)
            {
                _spawner.ClearPool();
            }

            _spawner = new ObjectSpawner<Enemy>((breakpoint as HordeBreakpoint).EnemyToSpawnPrefab, (breakpoint as HordeBreakpoint).SpawnCount, transform);

            for (int i = 0; i < (breakpoint as HordeBreakpoint).SpawnCount; i++)
            {
                Spawn(GetSpawnPosition());
                _spawned++;
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

    protected override void Spawn(Vector3 position)
    {
        Enemy spawnedEnemy = _spawner.Spawn(position);

        if (spawnedEnemy != null)
        {
            spawnedEnemy.Initialize(_player, _spawner);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        Vector3 playerPos = _player.transform.position;

        return new Vector3
            (
                Cos(_spawned) * _spawnDeltaDistance + playerPos.x,
                playerPos.y,
                Sin(_spawned) * _spawnDeltaDistance + playerPos.z
            );
    }

    public void OnBossEvent()
    {
        if (_spawner == null || _spawner.SpawnCount == 0) return;

        for (int i = 0; i < _spawner.SpawnCount; i++)
        {
            if (_spawner.SpawnedObjects[i] != null)
            {
                _spawner.Release(_spawner.SpawnedObjects[i]);
            }
        }

        _spawner.SpawnedObjects.Cleanup();
        _onBossEvent = true;
    }

    public void OnBossEventEnd()
    {
        _onBossEvent = false;
    }

    private void DispelUpgrades()
    {
        if (_currentUpgrade == null)
        {
            if (_isDebug) Debug.Log("Current upgrade is null!");

            return;
        }

        if (_spawner != null)
        {
            foreach (Enemy zombie in _spawner.Objects)
            {
                zombie?.DispelUpgrade(_currentUpgrade);
            }

            foreach (Enemy zombie in _spawner.SpawnedObjects.List)
            {
                zombie?.DispelUpgrade(_currentUpgrade);
            }
        }
    }

    /// <summary>
    /// Add upgrade to enemies 
    /// </summary>
    private void GetUpgrade()
    {
        if (_currentUpgrade == null)
        {
            if (_isDebug) Debug.Log("Try get null upgrade!");

            return;
        }

        if (_spawner != null)
        {
            foreach (Enemy zombie in _spawner.Objects)
            {
                zombie?.GetUpgrade(_currentUpgrade);
            }

            foreach (Enemy zombie in _spawner.SpawnedObjects.List)
            {
                zombie?.GetUpgrade(_currentUpgrade);
            }
        }
    }
}
