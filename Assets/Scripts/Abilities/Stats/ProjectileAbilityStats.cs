using UnityEngine;

[System.Serializable]
public class ProjectileAbilityStats : WeaponAbilityStats
{
    [Header("Projectiles settings")]
    [SerializeField] protected Projectile _projectilePrefab;
    [Tooltip("Destroy projectiles that throwed when attacks")]
    [SerializeField] protected bool _destroyProjectilesOnAttack;
    [SerializeField] protected Radius _projectileSize;
    [SerializeField] protected Duration _projectileLifeDuration;

    [Space(10)]
    [SerializeField] protected ProjectileNumber _projectileNumber;
    [SerializeField] protected Cooldown _projectilesSpawnInterval;
    [SerializeField] protected ProjectileSpeed _projectileSpeed;
    [SerializeField] protected PenetrationNumber _penetrationNumber;

    public Projectile Projectile => _projectilePrefab;
    public Radius ProjectileSize => _projectileSize;
    public Duration ProjectileLifeDuration => _projectileLifeDuration;
    public bool DestroyProjectilesOnAttack => _destroyProjectilesOnAttack;
    public ProjectileNumber ProjectileNumber => _projectileNumber;
    public Cooldown ProjectilesSpawnInterval => _projectilesSpawnInterval;
    public ProjectileSpeed ProjectileSpeed => _projectileSpeed;
    public PenetrationNumber PenetrationNumber => _penetrationNumber;

    public override void Initialize()
    {
        base.Initialize();

        _projectileSize.Initialize();
        _projectileLifeDuration.Initialize();
        _projectileNumber.Initialize();
        _projectilesSpawnInterval.Initialize();
        _projectileSpeed.Initialize();
        _penetrationNumber.Initialize();
    }

    public override void GetUpgrade(Upgrade upgrade)
    {
        base.GetUpgrade(upgrade);

        _projectileSize.Upgrade(upgrade);
        _projectileLifeDuration.Upgrade(upgrade);
        _projectileNumber.Upgrade(upgrade);
        _projectilesSpawnInterval.Upgrade(upgrade);
        _projectileSpeed.Upgrade(upgrade);
        _penetrationNumber.Upgrade(upgrade);
    }

    public override void DispelUpgrade(Upgrade upgrade)
    {
        base.DispelUpgrade(upgrade);

        _projectileSize.DispelUpgrade(upgrade);
        _projectileLifeDuration.DispelUpgrade(upgrade);
        _projectileNumber.DispelUpgrade(upgrade);
        _projectilesSpawnInterval.DispelUpgrade(upgrade);
        _projectileSpeed.DispelUpgrade(upgrade);
        _penetrationNumber.DispelUpgrade(upgrade);
    }
}
