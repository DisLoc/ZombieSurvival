using UnityEngine;

[System.Serializable]
public class TargetAbility : WeaponAbility
{
    [SerializeField] protected ProjectileAbilityStats _stats;
    [SerializeField] protected AimType _aimType;

    public override AbilityStats Stats => _stats;

    public override bool Upgrade(Upgrade upgrade)
    {
        if (base.Upgrade(upgrade))
        {
            _stats.GetUpgrade(upgrade);

            return true;
        }
        else return false;
    }

    protected enum AimType
    {
        CustomTrajectory,
        RadialTrajectory,
        RandomTrajectory,
        NearestTarget
    }
}
