using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField] protected Projectile _projectilePrefab;
    [SerializeField] protected ProjectileNumber _projectileNumber;
    [SerializeField] protected ProjectileSpeed _projectileSpeed;

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }
}
