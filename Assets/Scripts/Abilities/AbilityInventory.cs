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

    public void Add(AbilityContainer ability)
    {
        if (ability as PassiveAbility != null && PassiveAbilitiesCount >= _maxPassiveAbilitiesCount)
        {
            Debug.Log("Add ability error! Max passive abilities count reached");

            return;
        } 

        if (ability as Weapon != null && ActiveAbilitiesCount >= _maxActiveAbilitiesCount)
        {
            Debug.Log("Add ability error! Max active abilities count reached");

            return;
        }

        ability.Initialize();

        _abilities.Add(ability);

        if (ability as Weapon != null)
        {
            Weapon weapon = Object.Instantiate(ability as Weapon, _abilitiesParent.position, Quaternion.identity, _abilitiesParent);

            _weapons.Add(weapon);
        }
    }
}
