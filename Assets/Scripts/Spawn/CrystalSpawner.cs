using UnityEngine;
using Zenject;

public class CrystalSpawner : MonoBehaviour, IEnemyKilledHandler, IGameStartHandler
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField][Range(0, 2)] private float _crystalDeltaYAxis = 0.1f;
    [SerializeField] private CrystalStats _crystalStats;
    [SerializeField] private int _poolSize;

    private FactoryMonoPool<ExpCrystal, ExpCrystal.Factory> _pool;

    [Inject] private ExpCrystal.Factory _crystalFactory;
    [Inject] private LevelBuilder _levelBuilder;

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
        _pool = new FactoryMonoPool<ExpCrystal, ExpCrystal.Factory>(_crystalStats.CrystalPrefab, _crystalFactory, _poolSize, transform);
    }

    /// <summary>
    /// Create ExpCrystal in position that Zombie dies
    /// </summary>
    /// <param name="zombie"></param>
    public void OnEnemyKilled(Zombie zombie)
    {
        ExpCrystal crystal = _pool.Pull();

        crystal.Initialize(_crystalStats.CrystalParams[0], _pool);

        Vector3 pos = zombie.transform.position;

        crystal.transform.position = new Vector3(pos.x, _levelBuilder.GridHeight + _crystalDeltaYAxis, pos.z);
    }
}
