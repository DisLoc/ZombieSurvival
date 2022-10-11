using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [Header("Ability settings")]
    [SerializeField] TargetAbility _ability;

    private MonoPool<Projectile> _pool;

    public override Ability Ability => _ability;

    public override void Initialize()
    {
        base.Initialize();

        //int x = (int)(_stats.AttackInterval.Value / _stats.ThrowDuration.Value);
        //_pool = new MonoPool<Projectile>(_stats.Projectile, (int)(x == 0 ? 1 : x * _stats.ProjectileNumber.Value));
    }

    public override void Attack(DamageableObject target)
    {
        base.Attack(target);

        if (_isReady)
        {
            if (_isDebug) Debug.Log(name + " attack " + target.name);
        }
    }
}