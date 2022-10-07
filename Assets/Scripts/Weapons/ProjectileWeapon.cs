using UnityEngine;

public class ProjectileWeapon : Weapon
{
    private MonoPool<Projectile> _pool;

    public override void Initialize()
    {
        base.Initialize();

        if (_stats.UseProjectiles)
        {
            int x = (int)(_stats.AttackInterval.Value / _stats.ThrowDuration.Value);
            _pool = new MonoPool<Projectile>(_stats.Projectile, (int)(x == 0 ? 1 : x * _stats.ProjectileNumber.Value));
        }
        else 
        {
            if (_isDebug) Debug.Log("Projectiles error!");

            _pool = new MonoPool<Projectile>(_stats.Projectile, 0);
        }
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