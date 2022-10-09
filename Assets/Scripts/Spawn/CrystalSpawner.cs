using UnityEngine;

public class CrystalSpawner : MonoBehaviour, IEnemyKilledHandler
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private CrystalStats _crystalStats;
    [SerializeField] private int _poolSize;

    private MonoPool<ExpCrystal> _pool;

    private void OnEnable()
    {
        EventBus.Subscribe(this);
        _pool = new MonoPool<ExpCrystal>(_crystalStats.CrystalPrefab, _poolSize);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
        _pool.ClearPool();
    }

    public void OnEnemyKilled(Zombie zombie)
    {
        ExpCrystal crystal = _pool.Pull();

        crystal.Initialize(_crystalStats.CrystalParams[0]);
        crystal.transform.position = zombie.transform.position;
    }
}
