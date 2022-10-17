using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ColliderWeapon : Weapon
{
    [SerializeField] protected WeaponAbilityStats _stats;

    public override AbilityStats Stats => _stats;

    public override void Initialize()
    {
        base.Initialize();

        _stats.Initialize();
    }

    public override void Attack()
    {
        base.Attack();
    }
}
