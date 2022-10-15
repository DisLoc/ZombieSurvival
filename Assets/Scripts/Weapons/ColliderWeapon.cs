using UnityEngine;

public class ColliderWeapon : Weapon
{
    [SerializeField] protected WeaponAbilityStats _stats;

    public override AbilityStats Stats => _stats;

    public override void Attack()
    {
        base.Attack();
    }
}
