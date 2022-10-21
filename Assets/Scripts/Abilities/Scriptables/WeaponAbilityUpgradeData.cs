using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Abilities/Weapon ability upgrade data", fileName = "New weapon ability upgrade data")]
public class WeaponAbilityUpgradeData : AbilityUpgradeData
{
    [SerializeField] protected Weapon _weapon;
    [Tooltip("Upgrades for each ability level. Require upgrade at level 0")]
    [SerializeField] protected List<CurrentUpgrade> _levelUpgrades;

    public List<CurrentUpgrade> Upgrades => _levelUpgrades;
    public Weapon Weapon => _weapon;
}
