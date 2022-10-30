using UnityEngine;
using Zenject;

public class CrystalSpawner : MonoBehaviour, IEnemyKilledHandler, IGameStartHandler, ILevelProgressUpdateHandler
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField][Range(0, 2)] private float _crystalDeltaYAxis = 0.2f;
    [SerializeField] private int _poolSize;

    private ObjectSpawner<ExpCrystal, ExpCrystal.Factory> _pool;
    private ChanceCombiner<CrystalParam> _spawnCombiner;
    private BreakpointList<CrystalBreakpoint> _breakpoints;

    [Inject] private ExpCrystal.Factory _crystalFactory;
    [Inject] private LevelContext _levelContext;

    private void OnEnable()
    {
        EventBus.Subscribe(this);

        _breakpoints = new BreakpointList<CrystalBreakpoint>(_levelContext.CrystalSpawnBreakpoints.Breakpoints);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
        ClearPool();
    }

    public void OnGameStart()
    {
        //ClearPool();
    }

    public void OnLevelProgressUpdate(int progress)
    {
        Breakpoint breakpoint = _breakpoints.CheckReaching(progress);

        if (breakpoint != null)
        {
            OnCrystalBreakpoint(breakpoint as CrystalBreakpoint);
        }
    }

    public void OnCrystalBreakpoint(CrystalBreakpoint breakpoint)
    {
        if (_isDebug) Debug.Log("Get breakpoint: " + breakpoint.Name);

        ClearPool();
        
        _pool = new ObjectSpawner<ExpCrystal, ExpCrystal.Factory>(breakpoint.SpawningCrystalsStats.CrystalPrefab, _crystalFactory, _poolSize, transform);
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
    public void OnEnemyKilled(Zombie zombie)
    {
        Vector3 pos = new Vector3(zombie.transform.position.x, _levelContext.LevelBuilder.GridHeight + _crystalDeltaYAxis, zombie.transform.position.z);

        ExpCrystal crystal = _pool.Spawn(pos);

        crystal.Initialize(_spawnCombiner.GetStrikedObject(), _pool);
    }
}
