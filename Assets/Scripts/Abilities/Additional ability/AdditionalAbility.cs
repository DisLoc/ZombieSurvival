using UnityEngine;

public class AdditionalAbility : AbilityContainer
{
    [SerializeField] protected AbilityStats _stats;
    [SerializeField] protected StatsAbilityUpgradeData _upgradeData;

    public override CurrentUpgrade CurrentUpgrade => _upgradeData.RepeatingUpgrade;
    public override AbilityStats Stats => _stats;
    public override AbilityUpgradeData UpgradeData => _upgradeData;

    public virtual void MakeEffect()
    {

    }
}
