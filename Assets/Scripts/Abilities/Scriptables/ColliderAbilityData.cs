using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Abilities/Collider ability data", fileName = "New stats collider data")]
public class ColliderAbilityData : WeaponAbilityData
{
    [SerializeField] protected WeaponAbilityStats _stats;

    public override AbilityStats Stats => _stats;
}
