using UnityEngine;
using Zenject;

public sealed class CrystalSpawner : Spawner, IEnemyKilledHandler
{
    [SerializeField] private int _poolSize;

    private ObjectSpawner<ExpCrystal> _pool;
    private ChanceCombiner<CrystalParam> _spawnCombiner;
    private BreakpointList<CrystalBreakpoint> _breakpoints;

    [Inject] private LevelContext _levelContext;
    [Inject] private Player _player;

    protected override void OnEnable()
    {
        base.OnEnable();

        _breakpoints = new BreakpointList<CrystalBreakpoint>(_levelContext.CrystalSpawnBreakpoints);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        ClearPool();
    }

    public override void OnLevelProgressUpdate(int progress)
    {
        Breakpoint breakpoint = _breakpoints.CheckReaching(progress);

        if (breakpoint != null)
        {
            OnCrystalBreakpoint(breakpoint as CrystalBreakpoint);
        }
    }

    private void OnCrystalBreakpoint(CrystalBreakpoint breakpoint)
    {
        if (_isDebug) Debug.Log("Get breakpoint: " + breakpoint.Name);

        ClearPool();
        
        _pool = new ObjectSpawner<ExpCrystal>(breakpoint.SpawningCrystalsStats.CrystalPrefab, _poolSize, transform);
        _spawnCombiner = new ChanceCombiner<CrystalParam>(breakpoint.SpawningCrystalsStats.CrystalSpawnParams);
    }

    private void ClearPool()
    {
        if (_pool != null)
        {
            _pool.ClearPool();
        }
    }

    /// <summary>
    /// Create ExpCrystal in position that Zombie dies
    /// </summary>
    /// <param name="zombie"></param>
    public void OnEnemyKilled(Enemy zombie)
    {
        Vector3 position = new Vector3
            (
                zombie.transform.position.x,
                _levelContext.LevelBuilder.GridHeight + _spawnDeltaDistance,
                zombie.transform.position.z
            );

        Spawn(position);
    }

    protected override void Spawn(Vector3 position)
    {
        ExpCrystal crystal = _pool.Spawn(position);

        crystal.Initialize(_spawnCombiner.GetStrikedObject(), _pool, _player);
    }
}
