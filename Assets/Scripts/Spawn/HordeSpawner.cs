using UnityEngine;
using Zenject;
using static UnityEngine.Mathf;

public class HordeSpawner : Spawner, IUpdatable, IFixedUpdatable, IBossEventHandler
{
    protected ObjectSpawner<Zombie> _spawner;
    protected BreakpointList<HordeBreakpoint> _breakpoints;

    private int _spawned;

    [Inject] private Player _player;
    [Inject] private LevelContext _levelContext;


    protected override void OnEnable()
    {
        base.OnEnable();

        _breakpoints = _levelContext.HordeBreakpoints;
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
            if (_isDebug) Debug.Log("Horde event incoming!");

            _spawned = 0;

            if (_spawner != null)
            {
                _spawner.ClearPool();
            }

            _spawner = new ObjectSpawner<Zombie>((breakpoint as HordeBreakpoint).EnemyToSpawnPrefab, (breakpoint as HordeBreakpoint).SpawnCount, transform);

            for (int i = 0; i < (breakpoint as HordeBreakpoint).SpawnCount; i++)
            {
                Spawn(GetSpawnPosition());
                _spawned++;
            }
        }
    }

    protected override void Spawn(Vector3 position)
    {
        Zombie spawnedEnemy = _spawner.Spawn(position);

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
    }
}
