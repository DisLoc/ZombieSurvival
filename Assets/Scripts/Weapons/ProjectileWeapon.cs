using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [Header("Ability settings")]
    [SerializeField] TargetAbilityData _ability;

    private MonoPool<Projectile> _pool;

    public override AbilityData Ability => _ability;

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