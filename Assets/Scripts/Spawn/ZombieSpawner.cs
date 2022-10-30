using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private List<Zombie> _zombies = new List<Zombie>();
    [SerializeField] private List<HordeBreakpoint> _hordeBreakpoints;
    [SerializeField] private List<HordeBreakpoint> _herdBreakpoints;

    [SerializeField] private LevelProgress _levelProgress;
    [SerializeField] private SpawnHorde _spawnHorde;

    private FactoryMonoPool<Zombie, Zombie.Factory> _pool;
    public FactoryMonoPool<Zombie, Zombie.Factory> Pool => _pool;


    [Inject] private Zombie.Factory _factory;
    [Inject] private LevelContext _levelContext;
    public LevelBuilder LevelBuilder => _levelContext.LevelBuilder;

    private bool _spawned;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), 5, 5);
        _pool = new FactoryMonoPool<Zombie, Zombie.Factory>(_zombies[0], _factory, 10);
        
    }

    private void Update()
    {/*
        if(_levelProgress.Value > _hordeBreakpoints[0].RequiredProgress && _spawned == false)
        {
            _spawnHorde.SetZombie(_zombies[Random.Range(0, _zombies.Count)]);
            EventBus.Publish<IStartHorde>(handler => handler.OnHordeSpawn());
            _spawned = true;
        }
        if(_levelProgress.Value > _herdBreakpoints[0].RequiredProgress && _spawned == false)
        {
            _spawnHorde.SetZombie(_zombies[Random.Range(0, _zombies.Count)]);
            EventBus.Publish<IStartHerd>(handler => handler.OnHerdSpawn());
   
        }*/
    }

    private void Spawn()
    {
        for (int i = 0; i < _zombies.Count; i++)
        {
            //Instantiate(zombies[Random.Range(0, zombies.Count)], new Vector3(0, 5, 0), Quaternion.identity, transform);
            var zombie = _pool.Pull();
            zombie.Initialize(_pool);
            zombie.gameObject.transform.position = new Vector3(0, LevelBuilder.GridHeight + 1f, 0);
        }
    }
}
