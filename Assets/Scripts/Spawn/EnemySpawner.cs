using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static UnityEngine.Mathf;

public class EnemySpawner : MonoBehaviour, IFixedUpdatable, ILevelProgressUpdateHandler, IBossEventHandler
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    [Header("Settings")]
    [SerializeField][Range(1, 30)] private float _spawnDistance;

    protected int _maxUnitsOnScene;
    private int _totalSpawned;

    private BreakpointList<EnemyBreakpoint> _enemyBreakpoints;
    private BreakpointList<HordeBreakpoint> _hordeBreakpoints;
    private BreakpointList<BossBreakpoint> _bossBreakpoints;

    private List<ObjectSpawner<Zombie, Zombie.Factory>> _spawners;
    private ChanceCombiner<Zombie> _combiner;

    #region Inject
    [Inject] private Zombie.Factory _factory;
    [Inject] private Player _player;
    [Inject] private LevelContext _levelContext;
    #endregion

    private void OnEnable()
    {
        EventBus.Subscribe(this);

        _enemyBreakpoints = _levelContext.EnemyBreakpoints;
        _hordeBreakpoints = _levelContext.HordeBreakpoints;
        _bossBreakpoints = _levelContext.BossBreakpoints;

        _spawners = new List<ObjectSpawner<Zombie, Zombie.Factory>>();
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    public void OnLevelProgressUpdate(int progress)
    {
        Breakpoint enemyBreakpoint = _enemyBreakpoints.CheckReaching(progress);
        Breakpoint hordeBreakpoint = _hordeBreakpoints.CheckReaching(progress);
        Breakpoint bossBreakpoint = _bossBreakpoints.CheckReaching(progress);

        if (enemyBreakpoint != null)
        {
            _combiner = new ChanceCombiner<Zombie>((enemyBreakpoint as EnemyBreakpoint).SpawningEnemies);
            _maxUnitsOnScene = (enemyBreakpoint as EnemyBreakpoint).MaxUnitsOnScene;

            ClearPools();

            foreach(ObjectChanceSpawn<Zombie> spawnChance in (enemyBreakpoint as EnemyBreakpoint).SpawningEnemies)
            {
                _spawners.Add(new ObjectSpawner<Zombie, Zombie.Factory>(spawnChance.Object, _factory, _maxUnitsOnScene, transform));
            }
            
        }

        if (hordeBreakpoint != null)
        {
            SpawnHorde(hordeBreakpoint as HordeBreakpoint);
        }

        if (bossBreakpoint != null)
        {
            SpawnBoss(bossBreakpoint as BossBreakpoint);
        }
    }

    private void FixedUpdate() // test
    {
        OnFixedUpdate();
    }

    public void OnFixedUpdate()
    {
        Spawn();
    }

    public void OnBossEvent()
    {

    }

    public virtual void Spawn()
    {
        Zombie enemy = _combiner.GetStrikedObject();

        if (enemy == null || _spawners == null) return;

        int unitsOnScene = 0;

        foreach(ObjectSpawner<Zombie, Zombie.Factory> pool in _spawners)
        {
            unitsOnScene += pool.SpawnCount;
        }

        if (unitsOnScene >= _maxUnitsOnScene)
        {
            return;
        }

        ObjectSpawner<Zombie, Zombie.Factory> spawner = null;

        foreach (ObjectSpawner<Zombie, Zombie.Factory> pool in _spawners)
        {
            if (pool.Prefab.Equals(enemy))
            {
                spawner = pool;
                break;
            }
        }

        if (spawner == null) return;



        Zombie spawnedEnemy = spawner.Spawn(GetSpawnPosition());

        if (spawnedEnemy != null)
        {
            spawnedEnemy.Initialize(spawner);
            _totalSpawned++;
        }

    }

    private Vector3 GetSpawnPosition()
    {
        Vector3 playerPos = _player.transform.position;
        int unitsOnScene = 0;

        foreach (ObjectSpawner<Zombie, Zombie.Factory> pool in _spawners)
        {
            unitsOnScene += pool.SpawnCount;
        }

        return new Vector3
            (
                Cos(_totalSpawned % _maxUnitsOnScene) * _spawnDistance + playerPos.x, 
                playerPos.y, 
                Sin(_totalSpawned % _maxUnitsOnScene) * _spawnDistance + playerPos.z
            );
    }

    public virtual void SpawnHorde(HordeBreakpoint breakpoint)
    {

    }

    public virtual void SpawnBoss(BossBreakpoint breakpoint)
    {

    }

    private void ClearPools()
    {
        if (_spawners != null)
        {
            foreach(ObjectSpawner<Zombie, Zombie.Factory> pool in _spawners)
            {
                pool.ClearPool();
            }

            _spawners.Clear();
        }
    }
}
