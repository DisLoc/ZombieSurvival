using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponAbility : Ability
{
    [Tooltip("Upgrades for each ability level")]
    [SerializeField] protected List<CurrentUpgrade> _levelUpgrades;

    public override CurrentUpgrade CurrentUpgrade => _levelUpgrades[(int)_level.Value];
}
