using UnityEngine;
using Zenject;

public class SpawnHorde : MonoBehaviour, IStartHorde
{ 
    private Enemy zombie;

    [SerializeField] private ZombieSpawner zombieSpawner;

    [Inject] private Player _player;


    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    public void SetZombie(Enemy zombieNew)
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
            zombie.Initialize(_player, pool);
            zombie.gameObject.transform.position = new Vector3(0, zombieSpawner.LevelBuilder.GridHeight + 1f, 0);
        }
    }

}
