using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponAbility : Ability
{
    [SerializeField] protected Weapon _weapon;

    [Tooltip("Upgrades for each ability level")]
    [SerializeField] protected List<CurrentUpgrade> _levelUpgrades;

    public override CurrentUpgrade CurrentUpgrade => _levelUpgrades[(int)Stats.Level.Value];
    public Weapon Weapon => _weapon;
}
