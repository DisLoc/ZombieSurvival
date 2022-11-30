using System.Collections.Generic;
using UnityEngine;
using Zenject;

using static UnityEngine.Mathf;

public sealed class CrystalSpawner : Spawner, IEnemyKilledHandler, IGameStartHandler
{
    [SerializeField] private int _poolSize;
    [SerializeField] private float _startSpawnMinRange = 2f;
    [SerializeField] private float _startSpawnMaxRange = 5f;

    private ObjectSpawner<ExpCrystal> _pool;
    private ChanceCombiner<CrystalParam> _spawnCombiner;
    private BreakpointList<CrystalBreakpoint> _breakpoints;

    [Inject] private Player _player;
    [Inject] private LevelContext _levelContext;

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

    public void OnGameStart()
    {
        var spawnCombiner = new ChanceCombiner<CrystalParam>(_levelContext.StartCrystalStats.CrystalSpawnParams);

        Vector3 playerPos = _player.transform.position;

        for(int i = 0; i < _levelContext.StartCrystalsCount; i++)
        {
            ExpCrystal crystal = Instantiate(_levelContext.StartCrystalStats.CrystalPrefab, transform);

            crystal.transform.position = new Vector3
                (
                    Cos(Random.Range(0f, 2 * PI)) * GetStartRange() + playerPos.x,
                    _levelContext.LevelBuilder.GridHeight,
                    Sin(Random.Range(0f, 2 * PI)) * GetStartRange() + playerPos.z
                );

            crystal.Initialize(spawnCombiner.GetStrikedObject(), null, _player);
        }
    }

    private float GetStartRange() => Random.Range(_startSpawnMinRange, _startSpawnMaxRange);

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

        List<ExpCrystal> crystals = null;

        if (_pool != null)
        {
            _pool.SpawnedObjects.Cleanup();
            crystals = _pool.SpawnedObjects.List;
        
            foreach (ExpCrystal crystal in crystals)
            {
                crystal.transform.parent = null;
            }
        }

        ClearPool();
        
        _pool = new ObjectSpawner<ExpCrystal>(breakpoint.SpawningCrystalsStats.CrystalPrefab, _poolSize, transform);
        _spawnCombiner = new ChanceCombiner<CrystalParam>(breakpoint.SpawningCrystalsStats.CrystalSpawnParams);

        if (crystals != null)
        {
            foreach (ExpCrystal crystal in crystals)
            {
                crystal.transform.parent = _pool.Parent;
                _pool.AddObject(crystal);
            }

            crystals.Clear();
        }
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
        if (!zombie.HasExpReward) return;

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
