using System.Collections.Generic;
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

[System.Serializable]
public class FatZombieExplosionStats : IObjectStats
{
    [SerializeField] private Damage _explosionDamage;
    [SerializeField] private Duration _explosionDuration;
    [SerializeField] private Radius _explosionRadius;

    public Damage ExplosionDamage => _explosionDamage;
    public Duration ExplosionDuration => _explosionDuration;
    public Radius ExplosionRadius => _explosionRadius;

    public void Initialize()
    {
        _explosionDamage.Initialize();
        _explosionDuration.Initialize();
        _explosionRadius.Initialize();

    }
    public void GetUpgrade(Upgrade upgrade)
    {
        _explosionDamage.Upgrade(upgrade);
        _explosionDuration.Upgrade(upgrade);
        _explosionRadius.Upgrade(upgrade);
    }

    public void DispelUpgrade(Upgrade upgrade)
    {
        _explosionDamage.DispelUpgrade(upgrade);
        _explosionDuration.DispelUpgrade(upgrade);
        _explosionRadius.DispelUpgrade(upgrade);
    }

    public void DispelUpgrades(List<Upgrade> upgrades)
    {
        foreach(Upgrade upgrade in upgrades)
        {
            DispelUpgrade(upgrade);
        }
    }
}
