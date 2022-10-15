using UnityEngine;

public abstract class ProjectileWeapon : Weapon
{
    [SerializeField] protected ProjectileAbilityStats _stats;

    protected MonoPool<Projectile> _pool;

    public override AbilityStats Stats => _stats;

    public override void Initialize()
    {
        base.Initialize();

        //int x = (int)(_stats.AttackInterval.Value / _stats.ThrowDuration.Value);
        //_pool = new MonoPool<Projectile>(_stats.Projectile, (int)(x == 0 ? 1 : x * _stats.ProjectileNumber.Value));
    }

    public override void Attack()
    {
        base.Attack();
    }
}