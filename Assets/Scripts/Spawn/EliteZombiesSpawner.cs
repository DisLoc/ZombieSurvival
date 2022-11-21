using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

using static UnityEngine.Mathf;

public sealed class EliteZombiesSpawner : EnemySpawner
{
    [SerializeField][Range(1, 5)] private int _poolSize;
    [SerializeField] private ZombieChest _chestPrefab;

    private ObjectSpawner<Enemy> _spawner;
    private BreakpointList<EliteZombieBreakpoint> _breakpoints;

    private BreakpointList<UpgradeBreakpoint> _upgradeBreakpoints;

    private Upgrade _currentUpgrade;
    private int _abilitiesRewardCount;

    private bool _onBossEvent;

    [Inject] private AbilityGiver _abilityGiver;

    protected override void OnEnable()
    {
        base.OnEnable();

        _breakpoints = new BreakpointList<EliteZombieBreakpoint>(_levelContext.EliteZombieBreakpoints);
        _upgradeBreakpoints = new BreakpointList<UpgradeBreakpoint>(_levelContext.EnemyUpgradeBreakpoints);
    }

    public override void OnUpdate()
    {
        if (_onBossEvent || _spawner == null || _spawner.SpawnCount == 0) return;

        for (int i = 0; i < _spawner.SpawnCount; i++)
        {
            _spawner.SpawnedObjects[i]?.OnUpdate();
        }
        _spawner.SpawnedObjects.Cleanup();

        TryClearPool();
    }

    public override void OnFixedUpdate()
    {
        if (_onBossEvent || _spawner == null || _spawner.SpawnCount == 0) return;

        for (int i = 0; i < _spawner.SpawnCount; i++)
        {
            if (_spawner.SpawnedObjects[i] != null)
            {
                _spawner.SpawnedObjects[i]?.OnFixedUpdate();

                Vector3 moveDirection = new Vector3
                    (
                        _player.transform.position.x - _spawner.SpawnedObjects[i].transform.position.x,
                        0f,
                        _player.transform.position.z - _spawner.SpawnedObjects[i].transform.position.z
                    );

                _spawner.SpawnedObjects[i]?.Move(moveDirection);
            }
            else continue;
        }

        _spawner.SpawnedObjects.Cleanup();

        TryClearPool();
    }

    public override void OnLevelProgressUpdate(int progress)
    {
        Breakpoint breakpoint = _breakpoints.CheckReaching(progress);

        if (breakpoint != null)
        {
            if (_isDebug) Debug.Log("Elite zombie incoming!");

            if (_spawner != null)
            {
                _spawner.ClearPool();
            }

            _abilitiesRewardCount = (breakpoint as EliteZombieBreakpoint).AbilitiesRewardCount;

            _spawner = new ObjectSpawner<Enemy>
                (
                    (breakpoint as EliteZombieBreakpoint).EnemyToSpawnPrefab,
                    _poolSize,
                    transform
                );

            for (int i = 0; i < _poolSize; i++)
            {
                Spawn(GetSpawnPosition());
            }

            DispelUpgrades();
            GetUpgrade();
        }

        Breakpoint upgradeBreakpoint = _upgradeBreakpoints.CheckReaching(progress);

        if (upgradeBreakpoint != null)
        {
            if (_isDebug) Debug.Log("Elite zombie upgrade!");

            DispelUpgrades();

            _currentUpgrade = (upgradeBreakpoint as UpgradeBreakpoint).Upgrade;

            GetUpgrade();
        }
    }

    protected override void Spawn(Vector3 position)
    {
        Enemy spawnedEnemy = _spawner.Spawn(position);

        spawnedEnemy.Initialize(_player, _spawner);
        (spawnedEnemy as EliteZombie).InitializeSpawner(this);
    }

    private Vector3 GetSpawnPosition()
    {
        Vector3 playerPos = _player.transform.position;

        return new Vector3
            (
                Cos(Random.Range(0f, 2*PI)) * _spawnDeltaDistance + playerPos.x,
                _levelContext.LevelBuilder.GridHeight + _spawner.Prefab.Collider.height * _spawner.Prefab.transform.localScale.y * 0.5f,
                Sin(Random.Range(0f, 2*PI)) * _spawnDeltaDistance + playerPos.z
            );
    }

    public void OnEliteZombieDies(EliteZombie zombie)
    {
        if (_chestPrefab != null)
        {
            ZombieChest chest = Instantiate(_chestPrefab, zombie.transform.position, _chestPrefab.transform.localRotation);
            chest.Initialize(_player, _abilityGiver, _abilitiesRewardCount);
        }
        else if (_isDebug) Debug.Log("Missing RewardChest!");
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
