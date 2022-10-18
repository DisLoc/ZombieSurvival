using UnityEngine;

public abstract class ProjectileWeapon : Weapon
{
    [SerializeField] protected ProjectileAbilityStats _stats;

    protected MonoPool<Projectile> _pool;

    public override AbilityStats Stats => _stats;

    public override void Initialize()
    {
        base.Initialize();

        _stats.Initialize();

        int x = (int)(_stats.AttackInterval.Value / _stats.ProjectileLifeDuration.Value);
        _pool = new MonoPool<Projectile>(_stats.Projectile, (x == 0 ? 1 : x) * (int)_stats.ProjectileNumber.Value);
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override bool Upgrade(Upgrade upgrade)
    {
        _stats.GetUpgrade(upgrade);

        return base.Upgrade(upgrade);
    }
}