using System.Collections.Generic;
using UnityEngine;

public class AbilityInventory
{
    private List<Weapon> _weapons;
    private List<AbilityUpgradeData> _abilities;
    private Transform _parent;

    public List<Weapon> Weapons => _weapons;
    public List<AbilityUpgradeData> Abilities => _abilities;

    public AbilityInventory(Transform parent)
    {
        /*
        _weapons = new List<Weapon>();
        _abilities = new List<AbilityData>();

        _parent = parent;
        */
    }

    public void Add(AbilityUpgradeData ability)
    {
        /*
        ability.Initialize();

        _abilities.Add(ability);

        if (ability as WeaponAbilityData != null)
        {
            Weapon weapon = Object.Instantiate((ability as WeaponAbilityData).Weapon, _parent.position, Quaternion.identity, _parent);

            _weapons.Add(weapon);
        }
        */
    }
}
