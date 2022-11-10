using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : CharacterBase
{
    [Header("Moving settings")]
    [SerializeField] protected PlayerMoveController _moveController;

    [Header("Colliders")]
    [Tooltip("Self collider")]
    [SerializeField] protected CapsuleCollider _collider;
    [SerializeField] protected ObjectCatcher _pickablesCatcher;
    
    [Header("Stats settings")]
    [SerializeField] protected PlayerStats _stats;

    [Header("Animations settings")]
    [SerializeField] protected Animator _animator;
    /// <summary>
    /// Current animation bool
    /// </summary>
    [HideInInspector] public bool isMoving;

    [Header("Inventory settings")]
    [SerializeField] protected AbilityInventory _abilityInventory;
    [SerializeField] protected CurrencyInventory _coinInventory;
    [SerializeField] protected EquipmentInventory _equipmentInventory;

    protected List<Upgrade> _upgrades;

    public override CharacterStats Stats => _stats;

    public AbilityInventory AbilityInventory => _abilityInventory;
    public CurrencyInventory CoinInventory => _coinInventory;

    [Inject] protected LevelContext _levelContext;

    public void Initialize()
    {
        transform.position = new Vector3(0, _levelContext.LevelBuilder.GridHeight + _collider.height * 0.5f, 0);

        _stats.Initialize();

        _healthBar.Initialize(_stats.Health);
        _pickablesCatcher.Initialize(_stats.PickUpRange);
        _abilityInventory.Initialize();
        _coinInventory.Initialize();

        _upgrades = new List<Upgrade>();

        GetAbility(_stats.BaseWeapon);
    }

    private void Update()
    {
        Attack();

        _animator.SetBool("IsMoving", isMoving);
    }

    private void FixedUpdate()
    {
        OnFixedUpdate();

        _moveController.OnFixedUpdate();

        foreach (ProjectileWeapon weapon in _abilityInventory.ProjectileWeapons)
        {
            weapon.OnFixedUpdate();
        }
    }

    public override void Move(Vector3 direction)
    {
        Vector3 pos = transform.position;

        transform.LookAt(pos + direction);
        transform.position = Vector3.MoveTowards(pos, pos + direction * _stats.Velocity.Value, _stats.Velocity.Value * Time.fixedDeltaTime);
    }

    protected override void Attack()
    {
        foreach(Weapon weapon in _abilityInventory.Weapons)
        {
            weapon.OnUpdate();
            weapon.Attack();
        }
    }

    /// <summary>
    /// Upgrade player stats and all abilities he has
    /// </summary>
    /// <param name="upgrade"></param>
    public override void GetUpgrade(Upgrade upgrade)
    {
        base.GetUpgrade(upgrade);
        _coinInventory.GetUpgrade(upgrade);

        _upgrades.Add(upgrade);

        _pickablesCatcher.UpdateRadius();

        for (int index = 0; index < _abilityInventory.Abilities.Count; index++)
        {
            _abilityInventory.Abilities[index].Upgrade(upgrade);
        }
    }

    /// <summary>
    /// Get new ability or upgrade existing
    /// </summary>
    /// <param name="ability"></param>
    /// <returns>Return added or upgraded ability</returns>
    public AbilityContainer GetAbility(AbilityContainer ability)
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
                        foreach(Upgrade upgrade in _upgrades)
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

            if (abilityContainer as PassiveAbility != null)
            {
                GetUpgrade(abilityContainer.CurrentUpgrade.Upgrade);
            }
            else if (abilityContainer as Weapon != null)
            {
                abilityContainer.Upgrade(abilityContainer.CurrentUpgrade.Upgrade);
            }
            else if (_isDebug) Debug.Log("Missing ability!");

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

    public override void DispelUpgrade(Upgrade upgrade)
    {
        base.DispelUpgrade(upgrade);

        foreach(AbilityContainer ability in _abilityInventory.Abilities)
        {
            ability.DispelUpgrade(upgrade);
        }
    }

    public override void Die()
    {
        base.Die();

        EventBus.Publish<IPlayerDieHandler>(handler => handler.OnPlayerDie());
    }
}
