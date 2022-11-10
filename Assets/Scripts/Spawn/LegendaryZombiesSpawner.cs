using Zenject;
using UnityEngine;

public sealed class LegendaryZombiesSpawner : EnemySpawner, ISpawnLegendaryZombie
{
    private Enemy _legendaryZombie;
    [SerializeField] ZombieSpawner zombieSpawner;

    [Inject] private Player _player;

    public void SetZombie(Enemy zombieNew)
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

    protected override void Spawn(Vector3 position)
    {
        throw new System.NotImplementedException();
    }

    protected override void GetUpgrade()
    {
        throw new System.NotImplementedException();
    }

    protected override void DispelUpgrades()
    {
        throw new System.NotImplementedException();
    }
}
