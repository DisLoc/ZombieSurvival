using UnityEngine;

[System.Serializable]
public class ProjectileAbilityStats : WeaponAbilityStats
{
    [SerializeField] protected Projectile _projectilePrefab;
    [SerializeField] protected Radius _projectileSize;
    [SerializeField] protected ProjectileNumber _projectileNumber;
    [SerializeField] protected ProjectileSpeed _projectileSpeed;
    [SerializeField] protected PenetrationNumber _penetrationNumber;

    public Projectile Projectile => _projectilePrefab;
    public Radius ProjectileSize => _projectileSize;
    public ProjectileNumber ProjectileNumber => _projectileNumber;
    public ProjectileSpeed ProjectileSpeed => _projectileSpeed;
    public PenetrationNumber PenetrationNumber => _penetrationNumber;

    public override void Initialize()
    {
        base.Initialize();

        _projectileSize.Initialize();
        _projectileNumber.Initialize();
        _projectileSpeed.Initialize();
        _penetrationNumber.Initialize();
    }

    public override void GetUpgrade(Upgrade upgrade)
    {
        base.GetUpgrade(upgrade);

        _projectileSize.Upgrade(upgrade);
        _projectileNumber.Upgrade(upgrade);
        _projectileSpeed.Upgrade(upgrade);
        _penetrationNumber.Upgrade(upgrade);
    }
}
