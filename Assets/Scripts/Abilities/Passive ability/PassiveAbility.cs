using System.Collections.Generic;
using UnityEngine;

public class PassiveAbility : AbilityContainer
{
    [SerializeField] protected StatsAbilityUpgradeData _upgradeData;
    [SerializeField] protected AbilityStats _stats;
    [SerializeField] protected List<CombineAbility> _combinedAbilities;

    public override AbilityUpgradeData UpgradeData => _upgradeData;
    public override AbilityStats Stats => _stats;
    public override CurrentUpgrade CurrentUpgrade => _upgradeData.RepeatingUpgrade;
    public List<CombineAbility> CombinedAbilities => _combinedAbilities;

    public Weapon FindCombine(Weapon weapon)
    {
        foreach(CombineAbility combine in _combinedAbilities)
        {
            if (weapon.Stats.AbilityMarker.Equals(combine.CombinedWeapon.Stats.AbilityMarker))
            {
                return combine.SuperWeapon;
            }
        }

        return null;
    }
}
