using UnityEngine;

public class ColliderWeapon : Weapon
{
    [SerializeField] protected ColliderAbilityData _ability;

    public override AbilityData Ability => _ability;

    public override void Attack()
    {
        base.Attack();
    }
}
