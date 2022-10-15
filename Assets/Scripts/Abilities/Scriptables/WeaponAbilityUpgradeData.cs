using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Abilities/Weapon ability upgrade data", fileName = "New weapon ability upgrade data")]
public class WeaponAbilityUpgradeData : AbilityUpgradeData
{
    [SerializeField] protected Weapon _weapon;

    [SerializeField] protected List<CurrentUpgrade> _levelUpgrades;

    public List<CurrentUpgrade> Upgrades => _levelUpgrades;
    public Weapon Weapon => _weapon;
}
