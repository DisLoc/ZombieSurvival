using UnityEngine;
using Zenject;

public class BossSpawner : Spawner
{
    [SerializeField] private int _poolSize = 1;

    private BreakpointList<BossBreakpoint> _breakpoints;
    private ObjectSpawner<Enemy> _spawner;
    private GameObject _fence;

    [Inject] private Player _player;
    [Inject] private LevelContext _levelContext;

    protected override void OnEnable()
    {
        base.OnEnable();

        _breakpoints = _levelContext.BossBreakpoints;
    }

    public override void OnUpdate()
    {
        if (_spawner == null || _spawner.SpawnCount == 0) return;

        for (int i = 0; i < _spawner.SpawnCount; i++)
        {
            _spawner.SpawnedObjects[i]?.OnUpdate();
        }
        _spawner.SpawnedObjects.Cleanup();
    }

    public override void OnFixedUpdate()
    {
        if (_spawner == null || _spawner.SpawnCount == 0) return;

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
            if (_spawner != null)
            {
                _spawner.ClearPool();
            }

            if (_isDebug) Debug.Log("Boss event incoming!");

            EventBus.Publish<IBossEventHandler>(handler => handler.OnBossEvent());

            Vector3 position = _player.transform.position;
            _spawner = new ObjectSpawner<Enemy>((breakpoint as BossBreakpoint).BossPrefab, _poolSize, transform);

            SpawnFence(position, (breakpoint as BossBreakpoint).BossEventFence);
            Spawn(position);
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
}
