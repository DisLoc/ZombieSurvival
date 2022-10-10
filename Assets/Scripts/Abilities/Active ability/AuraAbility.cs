using UnityEngine;

[System.Serializable]
public class AuraAbility : WeaponAbility
{
    [SerializeField] protected WeaponAbilityStats _stats;

    public override AbilityStats Stats => _stats;

}
