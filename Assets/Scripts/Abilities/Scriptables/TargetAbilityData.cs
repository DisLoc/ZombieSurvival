using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Abilities/Target ability data", fileName = "New target ability data")]
public class TargetAbilityData : WeaponAbilityData
{
    [SerializeField] protected ProjectileAbilityStats _stats;
    [SerializeField] protected AimType _aimType;

    public override AbilityStats Stats => _stats;

    protected enum AimType
    {
        CustomTrajectory,
        RadialTrajectory,
        RandomTrajectory,
        NearestTarget
    }
}
