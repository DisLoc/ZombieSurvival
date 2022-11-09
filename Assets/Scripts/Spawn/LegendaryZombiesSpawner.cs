using Zenject;
using UnityEngine;

public class LegendaryZombiesSpawner : EnemySpawner, ISpawnLegendaryZombie
{
    private Zombie _legendaryZombie;
    [SerializeField] ZombieSpawner zombieSpawner;

    [Inject] private Player _player;

    public void SetZombie(Zombie zombieNew)
    {
        _legendaryZombie = zombieNew;
    }



    public void SpawnLegendaryZombie()
    {
        var pool = zombieSpawner.Pool;
        var zombie = pool.Pull();
        zombie.Initialize(_player, pool);
        zombie.gameObject.transform.position = new Vector3(0, zombieSpawner.LevelBuilder.GridHeight + 1f, 0);
    }
}
