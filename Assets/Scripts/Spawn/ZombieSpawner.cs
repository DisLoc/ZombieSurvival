using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _zombies = new List<Enemy>();
    [SerializeField] private List<HordeBreakpoint> _hordeBreakpoints;
    [SerializeField] private List<HordeBreakpoint> _herdBreakpoints;
    [SerializeField] private List<HordeBreakpoint> _legendaryZombieBreakpoints;

    [SerializeField] private LevelProgress _levelProgress;
    [SerializeField] private SpawnHorde _spawnHorde;

    private MonoPool<Enemy> _pool;
    public MonoPool<Enemy> Pool => _pool;

    // factory installer dont work on Enemy.Factoy, spawn only 1 type of zombie
    //[Inject] private Enemy.Factory _factory;
    [Inject] private LevelContext _levelContext;
    [Inject] private Player _player;

    public LevelBuilder LevelBuilder => _levelContext.LevelBuilder;

    private bool _spawned;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), 5, 5);
        _pool = new MonoPool<Enemy>(_zombies[0], 10);
        
    }

    private void Update()
    {
        if (_levelProgress.Value > _hordeBreakpoints[0].RequiredProgress && _spawned == false)
        {
            _spawnHorde.SetZombie(_zombies[Random.Range(0, _zombies.Count)]);
            EventBus.Publish<IStartHorde>(handler => handler.OnHordeSpawn());
            _spawned = true;
        }
        if (_levelProgress.Value > _herdBreakpoints[0].RequiredProgress && _spawned == false)
        {
            _spawnHorde.SetZombie(_zombies[Random.Range(0, _zombies.Count)]);
            EventBus.Publish<IStartHerd>(handler => handler.OnStartHerd());

        }

        if (_levelProgress.Value > _legendaryZombieBreakpoints[0].RequiredProgress && _spawned == false)
        {
            _spawnHorde.SetZombie(_zombies[Random.Range(0, _zombies.Count)]);
            EventBus.Publish<ISpawnLegendaryZombie>(handler => handler.SpawnLegendaryZombie());

        }
    }

    private void Spawn()
    {
        for (int i = 0; i < _zombies.Count; i++)
        {
            //Instantiate(zombies[Random.Range(0, zombies.Count)], new Vector3(0, 5, 0), Quaternion.identity, transform);
            var zombie = _pool.Pull();
            zombie.Initialize(_player, _pool);
            zombie.gameObject.transform.position = new Vector3(0, LevelBuilder.GridHeight + 1f, 0);
        }
    }
}
