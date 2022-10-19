using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public sealed class AbilityInventory
{
    [SerializeField] private int _maxActiveAbilitiesCount;
    [SerializeField] private int _maxPassiveAbilitiesCount;

    [SerializeField] private Transform _abilitiesParent;

    private List<Weapon> _weapons;
    private List<AbilityContainer> _abilities;

    public List<Weapon> Weapons => _weapons;
    public List<AbilityContainer> Abilities => _abilities;
    public int PassiveAbilitiesCount => _abilities.FindAll(item => item as PassiveAbility != null).Count;
    public int ActiveAbilitiesCount => _weapons.Count;

    public void Initialize()
    {
        _weapons = new List<Weapon>();
        _abilities = new List<AbilityContainer>();
    }

    public AbilityContainer Add(AbilityContainer ability)
    {
        if (ability as PassiveAbility != null && PassiveAbilitiesCount >= _maxPassiveAbilitiesCount)
        {
            return null;
        } 

        if (ability as Weapon != null && ActiveAbilitiesCount >= _maxActiveAbilitiesCount)
        {
            return null;
        }

        AbilityContainer newAbility = Object.Instantiate(ability, _abilitiesParent.position, Quaternion.identity, _abilitiesParent);

        newAbility.Initialize();

        _abilities.Add(newAbility);

        if (newAbility as Weapon != null)
        {
            _weapons.Add(newAbility as Weapon);
        }

        return newAbility;
    }

    public AbilityContainer Find(AbilityContainer ability)
    {
        return _abilities.Find(item => item.Name == ability.Name);
    }
}
