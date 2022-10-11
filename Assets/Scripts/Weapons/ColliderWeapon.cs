using UnityEngine;

public class ColliderWeapon : Weapon
{
    [SerializeField] protected AuraAbility _ability;

    public override Ability Ability => _ability;

    public override void Attack(DamageableObject target)
    {
        base.Attack(target);
    }
}
