using UnityEngine;
using Zenject;

public class CrystalSpawner : MonoBehaviour, IEnemyKilledHandler, IGameStartHandler
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField][Range(0, 2)] private float _crystalDeltaYAxis = 0.2f;
    [SerializeField] private CrystalStats _crystalStats;
    [SerializeField] private int _poolSize;

    private ObjectSpawner<ExpCrystal, ExpCrystal.Factory> _pool;

    [Inject] private ExpCrystal.Factory _crystalFactory;
    [Inject] private LevelBuilder _levelBuilder;

    [Inject] private LevelContext _levelContext;

    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
        _pool.ClearPool();
    }

    public void OnGameStart()
    {
        if (_pool != null) _pool.ClearPool();
        _pool = new ObjectSpawner<ExpCrystal, ExpCrystal.Factory>(_crystalStats.CrystalPrefab, _crystalFactory, _poolSize, transform);
    }

    /// <summary>
    /// Create ExpCrystal in position that Zombie dies
    /// </summary>
    /// <param name="zombie"></param>
    public void OnEnemyKilled(Zombie zombie)
    {
        Vector3 pos = new Vector3(zombie.transform.position.x, _levelBuilder.GridHeight + _crystalDeltaYAxis, zombie.transform.position.z);

        ExpCrystal crystal = _pool.Spawn(pos);

        //crystal.Initialize(_levelContext. _crystalStats.CrystalParams[0], _pool);
    }
}
