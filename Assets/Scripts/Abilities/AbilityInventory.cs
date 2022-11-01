using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public sealed class AbilityInventory
{
    [SerializeField] private int _maxActiveAbilitiesCount;
    [SerializeField] private int _maxPassiveAbilitiesCount;

    [SerializeField] private Transform _abilitiesParent;

    private List<Weapon> _weapons;
    private List<ProjectileWeapon> _projectileWeapons;
    private List<AbilityContainer> _abilities;

    /// <summary>
    /// Weapons that player getted in game
    /// </summary>
    public List<Weapon> Weapons => _weapons;
    /// <summary>
    /// Projectile weapons that player getted in game
    /// </summary>
    public List<ProjectileWeapon> ProjectileWeapons => _projectileWeapons;
    /// <summary>
    /// All abilities player getted in game
    /// </summary>
    public List<AbilityContainer> Abilities => _abilities;
    /// <summary>
    /// Count of passive abilities in inventory
    /// </summary>
    public int PassiveAbilitiesCount => _abilities.FindAll(item => item as PassiveAbility != null).Count;
    /// <summary>
    /// Count of weapons in inventory
    /// </summary>
    public int ActiveAbilitiesCount => _weapons.Count;
    /// <summary>
    /// Inventory capacity for passive abilities
    /// </summary>
    public int MaxPassiveAbilitiesCount => _maxPassiveAbilitiesCount;
    /// <summary>
    /// Inventory capacity for weapons
    /// </summary>
    public int MaxActiveAbilitiesCount => _maxActiveAbilitiesCount;

    public void Initialize()
    {
        _weapons = new List<Weapon>();
        _projectileWeapons = new List<ProjectileWeapon>();
        _abilities = new List<AbilityContainer>();
    }

    /// <summary>
    /// Try add ability to inventory
    /// </summary>
    /// <param name="ability">Ability need to add</param>
    /// <returns>Return added ability or null if cant add</returns>
    public AbilityContainer Add(AbilityContainer ability)
    {
        if (ability as PassiveAbility != null && PassiveAbilitiesCount >= _maxPassiveAbilitiesCount)
        {
            return null; // cant have too much abilities
        } 

        if (ability as Weapon != null && ActiveAbilitiesCount >= _maxActiveAbilitiesCount)
        {
            return null; // cant have too much abilities
        }

        AbilityContainer newAbility = Object.Instantiate(ability, _abilitiesParent);

        newAbility.Initialize();

        _abilities.Add(newAbility);

        if (newAbility as Weapon != null) // add to weapon list
        {
            _weapons.Add(newAbility as Weapon); 

            if (newAbility as ProjectileWeapon != null)
            {
                _projectileWeapons.Add(newAbility as ProjectileWeapon);
            }
        }

        return newAbility;
    }

    /// <summary>
    /// Remove ability from inventory
    /// </summary>
    /// <param name="ability">Ability need to remove</param>
    /// <returns>Return true if ability removed successfully</returns>
    public bool Remove(AbilityContainer ability)
    {
        AbilityContainer removingAbility = Find(ability);

        if (removingAbility != null)
        {
            if (removingAbility as Weapon != null)
            {
                _weapons.Remove(removingAbility as Weapon);
            }
            if (removingAbility as ProjectileWeapon != null)
            {
                _projectileWeapons.Remove(removingAbility as ProjectileWeapon);
            }

            return _abilities.Remove(removingAbility);
        }
        else return false;
    }

    /// <summary>
    /// Find ability in inventory by name
    /// </summary>
    /// <param name="ability">Ability need to find</param>
    /// <returns>Return existing ability or null if ability not in inventory</returns>
    public AbilityContainer Find(AbilityContainer ability)
    {
        if (ability == null) return null;
        return _abilities.Find(item => item.Name == ability.Name);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="weapon"></param>
    /// <returns></returns>
    public Weapon FindCombine(Weapon weapon)
    {
        if (weapon.IsSuper)
        {
            foreach(AbilityContainer ability in _abilities)
            {
                if (ability as PassiveAbility != null)
                {
                    foreach(CombineAbility combine in (ability as PassiveAbility).CombinedAbilities)
                    {
                        if (combine.SuperWeapon.Equals(weapon))
                        {
                            return combine.CombinedWeapon;
                        }
                        else continue;
                    }
                }
                else continue;
            }

            return null;
        }
        else
        {
            AbilityContainer ability = Find(weapon);

            if (ability != null)
            {
                if (ability.IsMaxLevel && Find((ability as Weapon).RequiredAbilityToUpgradeToSuper) != null)
                {
                    return (ability as Weapon).RequiredAbilityToUpgradeToSuper.FindCombine(ability as Weapon);
                }
                else return null;
            }
            else return null;
        }
    }
}
