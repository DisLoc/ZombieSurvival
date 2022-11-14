using UnityEngine;

public sealed class EliteZombie : Enemy
{
    private EliteZombiesSpawner _spawner;

    public void InitializeSpawner(EliteZombiesSpawner spawner)
    {
        _spawner = spawner;
    }

    public override void Die()
    {
        _spawner.OnEliteZombieDies(this);

        base.Die();
    }
}
