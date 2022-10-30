using UnityEngine;
using Zenject;

public class SpawnHorde : MonoBehaviour, IStartHorde
{ 
    private Zombie zombie;

    [SerializeField] private ZombieSpawner zombieSpawner;

    [Inject] private Player _player;
    [Inject] private LevelBuilder _levelBuilder;


    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
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
            var pool = zombieSpawner.Pool;
            var zombie = pool.Pull();
            zombie.Initialize(pool);
            zombie.gameObject.transform.position = new Vector3(0, zombieSpawner._levelBuilder.GridHeight + 1f, 0);
        }
    }

}
