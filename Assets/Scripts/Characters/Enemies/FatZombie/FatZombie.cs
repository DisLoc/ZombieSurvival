using UnityEngine;

public class FatZombie : Enemy
{
    [Header("Fat zombie settings")]
    [SerializeField] private TargetDetector _collisionTargetDetector;
    [SerializeField] private Radius _collisionDetectorRadius;

    [SerializeField] private FatZombieExplosion _explosion;
    [SerializeField] private FatZombieExplosionStats _explosionStats;

    public override void Initialize(Player player, MonoPool<Enemy> pool = null)
    {
        base.Initialize(player, pool);

        _collisionDetectorRadius.Initialize();
        _collisionTargetDetector.Initialize(_collisionDetectorRadius);

        _explosionStats.Initialize();
    }

    public override void Die()
    {
        FatZombieExplosion explosion = Instantiate(_explosion, transform.position, Quaternion.identity);
        explosion.Initialize(_explosionStats, _collisionTargetDetector.TriggerTags);

        base.Die();
    }

    protected override void Attack()
    {
        if (_collisionTargetDetector.Targets.Count > 0)
        {
            if (_isDebug) Debug.Log(name + " on collision");

            Die();
        }
        else
        {
            base.Attack();
        }
    }

    public override void GetUpgrade(Upgrade upgrade)
    {
        base.GetUpgrade(upgrade);

        _collisionDetectorRadius.Upgrade(upgrade);
        _collisionTargetDetector.UpdateRadius();

        _explosionStats.GetUpgrade(upgrade);
    }

    public override void DispelUpgrade(Upgrade upgrade)
    {
        base.DispelUpgrade(upgrade);

        _collisionDetectorRadius.DispelUpgrade(upgrade);
        _collisionTargetDetector.UpdateRadius();

        _explosionStats.DispelUpgrade(upgrade);
    }
}
