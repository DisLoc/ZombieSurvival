using UnityEngine;
using Zenject;

public sealed class BossSpawner : EnemySpawner
{
    [SerializeField][Range(1, 5)] private int _spawnCount = 1;
    [SerializeField] private GameObject _bossEventFencePrefab;

    [Header("Rewards")]
    [SerializeField][Range(1, 3)] private float _rewardsSpawnDistanceMultiplier = 1.5f;
    [SerializeField] private ZombieChest _chestPrefab;
    [SerializeField] private PickableHeart _heartPrefab;
    [SerializeField] private PickableMagnet _magnetPrefab;

    [SerializeField] private EquipmentList _equipmentList;

    private enum CurrentReward
    {
        PickablesReward,
        LevelPassReward
    }

    private CurrentReward _currentReward;

    private GameObject _fence;

    private BreakpointList<BossBreakpoint> _breakpoints;
    private BossBreakpoint _currentBreakpoint;

    [Inject] private AbilityGiver _abilityGiver;
    [Inject] private MainInventory _mainInventory;

    protected override void OnEnable()
    {
        base.OnEnable();

        _breakpoints = new BreakpointList<BossBreakpoint>(_levelContext.BossBreakpoints);
    }

    public override void OnUpdate()
    {
        if (CurrentSpawned > 0)
        {
            base.OnUpdate();
        }
        else return;
    }

    public override void OnFixedUpdate()
    {
        if (CurrentSpawned > 0)
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
            _currentBreakpoint = (breakpoint as BossBreakpoint);

            if (_isDebug) Debug.Log("Boss event incoming!");

            EventBus.Publish<IBossEventHandler>(handler => handler.OnBossEvent());

            Vector3 position = _player.transform.position;
            _spawners.Add(new ObjectSpawner<Enemy>(_currentBreakpoint.BossPrefab, _spawnCount, transform));

            SpawnFence(position, _bossEventFencePrefab);
            
            for (int i = 0; i < _spawnCount; i++)
            {
                Spawn(GetSpawnPosition());
            }

            GetUpgrade();

            if (_currentBreakpoint.IsFinalBoss)
            {
                _currentReward = CurrentReward.LevelPassReward;
            }
            else
            {
                _currentReward = CurrentReward.PickablesReward;
            }
        }

        base.OnLevelProgressUpdate(progress);
    }

    public void OnBossDies(Vector3 position)
    {
        ClearPools();

        if (_fence != null)
        {
            Destroy(_fence);
            _fence = null;
        }

        if (_currentReward.Equals(CurrentReward.PickablesReward))
        {
            if (_isDebug) Debug.Log("Get boss reward");

            Vector3 pickablesPos = new Vector3(position.x, _levelContext.LevelBuilder.GridHeight, position.z);

            PickableHeart heart = Instantiate(_heartPrefab, pickablesPos + Vector3.right * _rewardsSpawnDistanceMultiplier, 
                                              _heartPrefab.transform.localRotation, transform);
            PickableMagnet magnet = Instantiate(_magnetPrefab, pickablesPos + Vector3.left * _rewardsSpawnDistanceMultiplier,
                                                _magnetPrefab.transform.localRotation, transform);
            ZombieChest chest = Instantiate(_chestPrefab, pickablesPos + Vector3.up * _rewardsSpawnDistanceMultiplier,
                                            _chestPrefab.transform.localRotation, transform);

            heart.Initialize(_player);
            magnet.Initialize();
            chest.Initialize(_player, _abilityGiver, _currentBreakpoint.MaxAbilitiesRewardCount);
        }
        else
        {
            if (_isDebug) Debug.Log("Get level reward");

            Equipment rewardEquipment;

            if (_currentBreakpoint.HasRandomEquipmentReward || _currentBreakpoint.SpecificEquipmentReward == null)
            {
                rewardEquipment = _equipmentList.GetRandomEquipment(_currentBreakpoint.RandomEquipmentRarity);
            }
            else
            {
                rewardEquipment = _currentBreakpoint.SpecificEquipmentReward;
            }

            if (rewardEquipment != null)
            {
                _mainInventory.Add(rewardEquipment);
            }
            else if (_isDebug) Debug.Log("Reward error! Missing equipment");
        }

        EquipmentMaterial rewardMaterial;

        if (_currentBreakpoint.HasRandomMaterialReward || _currentBreakpoint.SpecificMaterialReward == null)
        {
            rewardMaterial = _equipmentList.GetRandomMaterial();
        }
        else
        {
            rewardMaterial = _currentBreakpoint.SpecificMaterialReward;
        }

        if (rewardMaterial != null)
        {
            _mainInventory.Add(rewardMaterial, _currentBreakpoint.MaterialsCount);
        }
        else if (_isDebug) Debug.Log("Reward error! Missing material");
        

        if (_isDebug) Debug.Log("Boss event ended");

        EventBus.Publish<IBossEventEndedHandler>(handler => handler.OnBossEventEnd());
    }

    protected override void Spawn(Vector3 position)
    {
        if (_spawners != null && _spawners.Count > 0)
        {
            Enemy boss = _spawners[0].Spawn(new Vector3
            (
                position.x,
                _levelContext.LevelBuilder.GridHeight + _spawners[0].Prefab.Collider.height * _spawners[0].Prefab.transform.localScale.y * 0.5f,
                position.z + _spawnDeltaDistance
            ));

            boss.Initialize(_player, _spawners[0]);
            (boss as BossZombie).InitializeSpawner(this); 
            
            _totalSpawned++;
        }
    }

    protected override Vector3 GetSpawnPosition()
    {
        Vector3 playerPos = _player.transform.position;

        return new Vector3
            (
                playerPos.x,
                0f,
                playerPos.z 
            ) + Vector3.forward;
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
}
