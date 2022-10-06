using UnityEngine;

[System.Serializable]
public class WeaponStats
{
    [SerializeField] protected AttackRange _attackRange;
    [SerializeField] protected Damage _damage;
    [SerializeField] protected Cooldown _attackInterval;

    public AttackRange AttackRange => _attackRange;
    public Damage Damage => _damage;
    public Cooldown AttackInterval => _attackInterval;

    [Space(10)]
    [SerializeField] private bool _useProjectiles;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private ProjectileSpeed _projectileSpeed;
    [SerializeField] private ProjectileNumber _projectileNumber;
    [SerializeField] private Duration _throwDuration;

    public bool UseProjectiles => _useProjectiles;
    public Projectile Projectile => _useProjectiles ? _projectile : null;
    public ProjectileSpeed ProjectileSpeed => _useProjectiles ? _projectileSpeed : null;
    public ProjectileNumber ProjectileNumber => _useProjectiles ? _projectileNumber : null;
    public Duration ThrowDuration => _useProjectiles ? _throwDuration : null;

    public void Initialize()
    {
        _attackRange.Initialize();
        _damage.Initialize();
        _attackInterval.Initialize();

        if (_useProjectiles)
        {
            _projectileSpeed.Initialize();
            _projectileNumber.Initialize();
            _throwDuration.Initialize();
        }
    }

    public virtual void GetUpgrade(Upgrade upgrade)
    {
        _attackRange.Upgrade(upgrade);
        _damage.Upgrade(upgrade);
        _attackInterval.Upgrade(upgrade);

        if (_useProjectiles)
        {
            _projectileSpeed.Upgrade(upgrade);
            _projectileNumber.Upgrade(upgrade);
            _throwDuration.Upgrade(upgrade);
        }
    }
}
