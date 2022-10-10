using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityInventory
{
    private List<Ability> _abilities;

    public List<Ability> Abilities => _abilities;

    public AbilityInventory()
    {
        _abilities = new List<Ability>();
    }

    public void Add(Ability ability)
    {
        _abilities.Add(ability);
    }

    public bool Remove(Ability ability)
    {
        return _abilities.Remove(ability);
    }
}
