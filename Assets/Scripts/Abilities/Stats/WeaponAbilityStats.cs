using UnityEngine;

[System.Serializable]
public class WeaponAbilityStats : AbilityStats
{
    [SerializeField] protected Damage _damage;
    [SerializeField] protected Cooldown _attackInterval;
    [SerializeField] protected Radius _attackRange;

    public Damage Damage => _damage;
    public Cooldown AttackInterval => _attackInterval;
    public Radius AttackRange => _attackRange;

    public override void Initialize()
    {
        base.Initialize();

        _damage.Initialize();
        _attackInterval.Initialize();
        _attackRange.Initialize();
    }

    public override void GetUpgrade(Upgrade upgrade)
    {
        base.GetUpgrade(upgrade);

        _damage.Upgrade(upgrade);
        _attackInterval.Upgrade(upgrade);
        _attackRange.Upgrade(upgrade);
    }
}
