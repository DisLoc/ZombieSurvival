using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ColliderWeapon : Weapon
{
    [SerializeField] protected WeaponAbilityStats _stats;

    public override AbilityStats Stats => _stats;

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
