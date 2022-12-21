using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : DamageableObject, IFixedUpdatable, IUpdatable
{
    [Tooltip("Can be null if HealthBar is null")]
    [SerializeField] protected HPBarCanvas _hpCanvas;

    [Header("Render settings")]
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] protected Vector3 _defaultViewSide = Vector3.right;

    [Header("Abilities settings")]
    [SerializeField] protected AbilityInventory _abilityInventory;

    protected List<Upgrade> _upgrades;

    /// <summary>
    /// Object stats
    /// </summary>
    public abstract CharacterStats Stats { get; }

    public override int HP => (int)Stats.Health.Value;
    public override int MaxHP => Stats.Health.MaxHP;

    /// <summary>
    /// Heal character based on fixedDeltaTime and regeneration value.
    /// Call OnFixedUpdate for each projectile ability in inventory
    /// </summary>
    public virtual void OnFixedUpdate()
    {
        Stats.Health.Heal(Stats.Regeneration.Value * Time.fixedDeltaTime);

        if (_healthBar != null) _healthBar.UpdateHealth();

        foreach (ProjectileWeapon weapon in _abilityInventory.ProjectileWeapons)
        {
            weapon.OnFixedUpdate();
        }
    }

    /// <summary>
    /// Character attacks with all Weapons it has
    /// </summary>
    public virtual void OnUpdate()
    {
        Attack();
    }

    /// <summary>
    /// Moving with current speed in direction
    /// </summary>
    /// <param name="direction">Normalized vector</param>
    public abstract void Move(Vector3 direction);

    protected virtual void Attack()
    {
        foreach (Weapon weapon in _abilityInventory.Weapons)
        {
            weapon.OnUpdate();
            weapon.Attack();
        }
    }

    public override void TakeDamage(int damage)
    {
        Stats.Health.TakeDamage(damage);

        base.TakeDamage(damage);
    }

    /// <summary>
    /// Get new ability or upgrade existing
    /// </summary>
    /// <param name="ability"></param>
    /// <returns>Returns added or upgraded ability</returns>
    public virtual AbilityContainer GetAbility(AbilityContainer ability)
    {
        if (ability as AdditionalAbility != null)
        {
            GetUpgrade((ability as AdditionalAbility).CurrentUpgrade.Upgrade);
            return ability;
        }

        if (ability as Weapon != null && (ability as Weapon).IsSuper)
        {
            Weapon weapon = _abilityInventory.FindCombine(ability as Weapon);

            if (weapon != null)
            {
                if (_isDebug) Debug.Log("Upgrade " + weapon.Name + " to super: " + ability.Name);

                if (_abilityInventory.Remove(weapon))
                {
                    AbilityContainer newAbility = _abilityInventory.Add(ability);

                    if (newAbility != null)
                    {
                        foreach (Upgrade upgrade in _upgrades)
                        {
                            newAbility.Upgrade(upgrade);
                        }
                    }
                }
            }
        }

        AbilityContainer abilityContainer = _abilityInventory.Find(ability);

        if (abilityContainer != null)
        {
            if (abilityContainer.IsMaxLevel)
            {
                if (_isDebug) Debug.Log("This ability is max level!");

                return abilityContainer;
            }

            if (_isDebug) Debug.Log("Ability already in inventory. Upgrade it");

            GetUpgrade(abilityContainer.CurrentUpgrade.Upgrade);

            return abilityContainer;
        }
        else
        {
            if (_isDebug) Debug.Log("Add new ability");

            AbilityContainer newAbility = _abilityInventory.Add(ability);

            if (newAbility != null)
            {
                foreach (Upgrade upgrade in _upgrades)
                {
                    newAbility.Upgrade(upgrade);
                }

                GetUpgrade(newAbility.CurrentUpgrade.Upgrade);
            }
            else if (_isDebug) Debug.Log("Adding ability error!");

            return newAbility;
        }
    }
    
    /// <summary>
    /// Upgrade character stats
    /// </summary>
    /// <param name="upgrade"></param>
    public virtual void GetUpgrade(Upgrade upgrade)
    {
        if (upgrade == null) return;

        Stats.GetUpgrade(upgrade);

        for (int index = 0; index < _abilityInventory.Abilities.Count; index++)
        {
            _abilityInventory.Abilities[index].Upgrade(upgrade);
        }

        _upgrades.Add(upgrade);
    }

    /// <summary>
    /// Dispel upgrade on stats
    /// </summary>
    /// <param name="upgrade"></param>
    public virtual void DispelUpgrade(Upgrade upgrade)
    {
        Stats.DispelUpgrade(upgrade);

        foreach (AbilityContainer ability in _abilityInventory.Abilities)
        {
            ability.DispelUpgrade(upgrade);
        }

        _upgrades.Remove(upgrade);
    }

    public virtual void DispelUpgrades(List<Upgrade> upgrades)
    {
        foreach(Upgrade upgrade in upgrades)
        {
            DispelUpgrade(upgrade);
        }
    }

    public void DestroyAbilities()
    {
        if (_abilityInventory != null && _abilityInventory.Abilities.Count > 0)
        {
            foreach(var ability in _abilityInventory.Abilities)
            {
                ability.DestroyAbility();
            }
        }
    }
}
