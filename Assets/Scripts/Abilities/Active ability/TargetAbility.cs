using UnityEngine;

[System.Serializable]
public class TargetAbility : WeaponAbility
{
    [SerializeField] protected ProjectileAbilityStats _stats;

    public override AbilityStats Stats => _stats;
}
