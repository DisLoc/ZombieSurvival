using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private List<Zombie> zombies = new List<Zombie>();
    [Inject] private Zombie.Factory factory;

    [Inject] public LevelBuilder levelBuilder;

    [SerializeField] HordeBreakpoint hordeBreakpoint;

    private FactoryMonoPool<Zombie, Zombie.Factory> pool;

    [SerializeField] private LevelProgress levelProgress;
    [SerializeField] private SpawnHorde spawnHorde;

    private bool spawned;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), 5, 5);
        pool = new FactoryMonoPool<Zombie, Zombie.Factory>(zombies[0], factory, 10);
        
    }

    private void Update()
    {
        if(levelProgress.Value > hordeBreakpoint.RequiredProgress && spawned == false)
        {
            spawnHorde.SetZombie(zombies[Random.Range(0, zombies.Count)]);
            EventBus.Publish<IStartHorde>(handler => handler.OnHordeSpawn());
            spawned = true;
        }
    }

    private void Spawn()
    {
        for (int i = 0; i < zombies.Count; i++)
        {
            //Instantiate(zombies[Random.Range(0, zombies.Count)], new Vector3(0, 5, 0), Quaternion.identity, transform);
            var zombie = pool.Pull();
            zombie.Initialize(pool);
            zombie.gameObject.transform.position = new Vector3(0, levelBuilder.GridHeight + 1f, 0);
        }


    }

    public FactoryMonoPool<Zombie, Zombie.Factory> ReturnPool()
    {
        return pool;
    }
}
