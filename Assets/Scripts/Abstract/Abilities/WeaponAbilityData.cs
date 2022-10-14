using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponAbilityData : AbilityData
{
    [SerializeField] protected Weapon _weapon;

    [SerializeField] protected List<CurrentUpgrade> _levelUpgrades;

    public List<CurrentUpgrade> Upgrades => _levelUpgrades;
    public Weapon Weapon => _weapon;
}
