using UnityEngine;
using Zenject;

using static UnityEngine.Mathf;

public sealed class EliteZombiesSpawner : EnemySpawner, IBossEventHandler
{
    [SerializeField][Range(1, 5)] private int _poolSize;
    [SerializeField] private ZombieChest _chestPrefab;

    private BreakpointList<EliteZombieBreakpoint> _breakpoints;

    private int _maxAbilitiesRewardCount;

    private bool _onBossEvent;

    [Inject] private AbilityGiver _abilityGiver;

    protected override void OnEnable()
    {
        base.OnEnable();

        _breakpoints = new BreakpointList<EliteZombieBreakpoint>(_levelContext.EliteZombieBreakpoints);
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
            if (_isDebug) Debug.Log("Elite zombie incoming!");

            _maxAbilitiesRewardCount = (breakpoint as EliteZombieBreakpoint).MaxAbilitiesRewardCount;

            _maxUnitsOnScene = _poolSize;

            DispelUpgrades();
            ReplacePools();

            _spawners.Add(new ObjectSpawner<Enemy>((breakpoint as EliteZombieBreakpoint).EnemyToSpawnPrefab, _maxUnitsOnScene, transform));

            for (int i = 0; i < _poolSize; i++)
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
            (spawnedEnemy as EliteZombie).InitializeSpawner(this); 
            
            _totalSpawned++;
        }
        else return;
    }

    protected override Vector3 GetSpawnPosition()
    {
        if (_spawners != null && _spawners.Count > 0)
        {
            Vector3 playerPos = _player.transform.position;

            return new Vector3
                (
                    Cos(Random.Range(0f, 2 * PI)) * _spawnDeltaDistance + playerPos.x,
                    _levelContext.LevelBuilder.GridHeight + _spawners[0].Prefab.Collider.height * _spawners[0].Prefab.transform.localScale.y * 0.5f,
                    Sin(Random.Range(0f, 2 * PI)) * _spawnDeltaDistance + playerPos.z
                );
        }
        else return -Vector3.one;
    }

    public void OnEliteZombieDies(EliteZombie zombie)
    {
        if (_chestPrefab != null)
        {
            ZombieChest chest = Instantiate(_chestPrefab, zombie.transform.position, _chestPrefab.transform.localRotation);
            chest.Initialize(_player, _abilityGiver, Random.Range(1, _maxAbilitiesRewardCount + 1));
        }
        else if (_isDebug) Debug.Log("Missing RewardChest!");
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

    private void TryClearPool()
    {
        if (CurrentSpawned == 0)
        {            
            ClearPools();
        }
    }
}
