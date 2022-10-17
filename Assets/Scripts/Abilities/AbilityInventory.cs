using System.Collections.Generic;
using UnityEngine;

public class AbilityInventory
{
    private List<Weapon> _weapons;
    private List<AbilityContainer> _abilities;
    private Transform _parent;

    public List<Weapon> Weapons => _weapons;
    public List<AbilityContainer> Abilities => _abilities;

    public AbilityInventory(Transform parent)
    {
        _weapons = new List<Weapon>();
        _abilities = new List<AbilityContainer>();

        _parent = parent;
    }

    public void Add(AbilityContainer ability)
    {
        ability.Initialize();

        _abilities.Add(ability);

        if (ability as Weapon != null)
        {
            Weapon weapon = Object.Instantiate(ability as Weapon, _parent.position, Quaternion.identity, _parent);

            _weapons.Add(weapon);
        }
    }
}
