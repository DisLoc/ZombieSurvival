using UnityEngine;

public sealed class BossSpawner : EnemySpawner
{
    [SerializeField][Range(1, 5)] private int _spawnCount = 1;
    [SerializeField] private ZombieChest _chestPrefab;
    [SerializeField] private GameObject _bossEventFencePrefab;

    private GameObject _fence;

    private BreakpointList<BossBreakpoint> _breakpoints;

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
            if (_isDebug) Debug.Log("Boss event incoming!");

            EventBus.Publish<IBossEventHandler>(handler => handler.OnBossEvent());

            Vector3 position = _player.transform.position;
            _spawners.Add(new ObjectSpawner<Enemy>((breakpoint as BossBreakpoint).BossPrefab, _spawnCount, transform));

            SpawnFence(position, _bossEventFencePrefab);
            
            for (int i = 0; i < _spawnCount; i++)
            {
                Spawn(GetSpawnPosition());
            }

            GetUpgrade();
        }

        base.OnLevelProgressUpdate(progress);
    }

    public void OnBossDies()
    {
        ClearPools();

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
