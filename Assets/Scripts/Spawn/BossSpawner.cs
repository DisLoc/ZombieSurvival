using UnityEngine;
using Zenject;

public class BossSpawner : Spawner
{
    [SerializeField] private int _poolSize = 1;

    private BreakpointList<BossBreakpoint> _breakpoints;
    private ObjectSpawner<Zombie> _spawner;

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
            _spawner = new ObjectSpawner<Zombie>((breakpoint as BossBreakpoint).BossPrefab, _poolSize, transform);

            SpawnFence(position, (breakpoint as BossBreakpoint).BossEventFence);
            Spawn(position);
        }
    }

    protected override void Spawn(Vector3 position)
    {
        Zombie boss = _spawner.Spawn(position + Vector3.forward * _spawnDeltaDistance);
        boss.Initialize(_player, _spawner);
    }

    private void SpawnFence(Vector3 position, GameObject fence)
    {

    }
}
