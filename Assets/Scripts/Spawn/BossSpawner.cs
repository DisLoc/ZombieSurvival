using UnityEngine;
using Zenject;

public sealed class BossSpawner : EnemySpawner
{
    [SerializeField][Range(1, 5)] private int _poolSize = 1;
    [SerializeField] private ZombieChest _chestPrefab;
    [SerializeField] private GameObject _bossEventFencePrefab;

    private ObjectSpawner<Enemy> _spawner;
    private GameObject _fence;

    private BreakpointList<BossBreakpoint> _breakpoints;
    private BreakpointList<UpgradeBreakpoint> _upgradeBreakpoints;

    private Upgrade _currentUpgrade;

    [Inject] private Player _player;
    [Inject] private LevelContext _levelContext;

    protected override void OnEnable()
    {
        base.OnEnable();

        _breakpoints = new BreakpointList<BossBreakpoint>(_levelContext.BossBreakpoints);
        _upgradeBreakpoints = new BreakpointList<UpgradeBreakpoint>(_levelContext.EnemyUpgradeBreakpoints);
    }

    public override void OnUpdate()
    {
        if (_spawner == null || _spawner.SpawnCount == 0) return;

        for (int i = 0; i < _spawner.SpawnCount; i++)
        {
            _spawner.SpawnedObjects[i]?.OnUpdate();
        }

        _spawner.SpawnedObjects.Cleanup();

        TryClearPool();
    }

    public override void OnFixedUpdate()
    {
        if (_spawner == null || _spawner.SpawnCount == 0) return;

        for (int i = 0; i < _spawner.SpawnCount; i++)
        {
            _spawner.SpawnedObjects[i]?.OnFixedUpdate();
        }

        _spawner.SpawnedObjects.Cleanup();

        TryClearPool();
    }

    public override void OnLevelProgressUpdate(int progress)
    {
        Breakpoint breakpoint = _breakpoints.CheckReaching(progress);

        if (breakpoint != null)
        {
            if (_spawner != null)
            {
                _spawner.ClearPool();
            }

            if (_isDebug) Debug.Log("Boss event incoming!");

            EventBus.Publish<IBossEventHandler>(handler => handler.OnBossEvent());

            Vector3 position = _player.transform.position;
            _spawner = new ObjectSpawner<Enemy>((breakpoint as BossBreakpoint).BossPrefab, _poolSize, transform);

            SpawnFence(position, _bossEventFencePrefab);
            Spawn(position);

            GetUpgrade();
        }

        Breakpoint upgradeBreakpoint = _upgradeBreakpoints.CheckReaching(progress);

        if (upgradeBreakpoint != null)
        {
            if (_isDebug) Debug.Log("Boss upgrade!");

            DispelUpgrades();

            _currentUpgrade = (upgradeBreakpoint as UpgradeBreakpoint).Upgrade;

            GetUpgrade();
        }
    }

    public void OnBossDies()
    {
        _spawner.ClearPool();
        _spawner = null;

        if (_fence != null)
        {
            Destroy(_fence);
            _fence = null;
        }

        if (_isDebug) Debug.Log("Boss event ended");

        EventBus.Publish<IBossEventEndedHandler>(handler => handler.OnBossEventEnd());
    }

    protected override void Spawn(Vector3 position)
    {
        Enemy boss = _spawner.Spawn(position + Vector3.forward * _spawnDeltaDistance + Vector3.up);
        boss.Initialize(_player, _spawner);
        (boss as BossZombie).InitializeSpawner(this);
    }

    private void SpawnFence(Vector3 position, GameObject fence)
    {
        if (fence != null)
        {
            _fence = Instantiate(fence, 
                                 new Vector3
                                 (
                                     position.x, 
                                     _levelContext.LevelBuilder.GridHeight + fence.transform.localScale.y * 0.5f,
                                     position.z
                                 ), fence.transform.localRotation, transform);
        }
    }

    private void TryClearPool()
    {
        if (_spawner.SpawnCount == 0)
        {
            _spawner.ClearPool();
            _spawner = null;
        }
    }

    protected override void GetUpgrade()
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

    protected override void DispelUpgrades()
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
}
