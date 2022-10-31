public class Boss : Zombie
{
    private BossSpawner _spawner;
    
    public void InitializeSpawner(BossSpawner spawner)
    {
        _spawner = spawner;
    }

    public override void Die()
    {
        base.Die();

        _spawner.OnBossDies();
    }
}
