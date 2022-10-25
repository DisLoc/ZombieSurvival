using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnHorde : MonoBehaviour, IStartHorde
{
    //[SerializeField] private List<>

    private Zombie zombie;

    [SerializeField] private ZombieSpawner zombieSpawner;


    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    public void SetZombie(Zombie zombieNew)
    {
        zombie = zombieNew;
    }

    public void OnHordeSpawn()
    {
        for (int i = 0; i < 20; i++)
        {
            //  Instantiate(zombie, transform);
            var pool = zombieSpawner.ReturnPool();
            var zombie = pool.Pull();
            zombie.Initialize(pool);
            zombie.gameObject.transform.position = new Vector3(0, zombieSpawner.levelBuilder.GridHeight + 1f, 0);
        }
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }
}
