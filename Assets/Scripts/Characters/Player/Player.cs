using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : CharacterBase
{
    [Header("Moving settings")]
    [SerializeField] protected PlayerMoveController _moveController;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected SpriteRenderer _moveDirectionArrow;

    [HideInInspector] public bool isMoving;

    [Header("Colliders")]
    [Tooltip("Self collider")]
    [SerializeField] protected CapsuleCollider _collider;
    [SerializeField] protected ObjectCatcher _pickablesCatcher;
    
    [Header("Stats settings")]
    [SerializeField] protected List<Weapon> _startWeaponsList;
    [SerializeField] protected PlayerUpgrades _levelUpgrades;

    [SerializeField] protected bool _useBaseWeapon;
    [SerializeField] protected PlayerStats _stats;

    [Header("Inventory settings")]
    [SerializeField] protected AbilityInventory _abilityInventory;
    [SerializeField] protected CurrencyInventory _coinInventory;

    protected List<Upgrade> _upgrades;

    public override CharacterStats Stats => _stats;

    public PlayerUpgrades LevelUpgrades => _levelUpgrades;
    public AbilityInventory AbilityInventory => _abilityInventory;
    public CurrencyInventory CoinInventory => _coinInventory;
    public Vector3 CameraDeltaPos => _moveController.CameraDeltaPos;


    [Inject] protected LevelContext _levelContext;
    [Inject] protected AbilityGiver _abilityGiver;
    [Inject] protected MainInventory _mainInventory;

    public void Initialize()
    {
        transform.position = new Vector3(0, _levelContext.LevelBuilder.GridHeight + _collider.height * 0.5f, 0);

        List<Upgrade> customUpgrades = new List<Upgrade>();

        if (!_useBaseWeapon)
        {
            List<Equipment> equip = _mainInventory.EquipmentInventory.Equipment.FindAll(item => item.isEquiped == true);
            
            if (equip.Count > 0)
            {
                foreach(Equipment equipment in equip)
                {
                    if (equipment != null)
                    {
                        if (equipment is WeaponEquipment weapon)
                        {
                            _stats.SetBaseWeapon(weapon.BaseWeapon);
                        }

                        customUpgrades.Add(equipment.StatsUpgrade);

                        foreach(Upgrade upgrade in equipment.RarityUpgrades)
                        {
                            customUpgrades.Add(upgrade);
                        }
                    }
                    else continue;
                }
            }
            else
            {
                List<Type> startWeaponTypes = new List<Type>();

                foreach (Weapon weapon in _startWeaponsList)
                {
                    startWeaponTypes.Add(weapon.GetType());
                }

                _stats.SetBaseWeapon(_abilityGiver.GetRandomWeapon(startWeaponTypes.Count > 0 ? startWeaponTypes : null));
            }
        }
        
        foreach(var upgrade in _mainInventory.CampInventory.CampUpgrades)
        {
            customUpgrades.Add(upgrade);
        }

        _stats.Initialize();

        if (_stats.BaseWeapon as Shotgun != null)
        {
            _animator.SetBool(AnimatorBools.WithShotgun.ToString(), true);
        }
        else if (_stats.BaseWeapon as Blade != null)
        {
            _animator.SetBool(AnimatorBools.WithBlade.ToString(), true);
        }
        else
        {
            _animator.SetBool(AnimatorBools.WithPistol.ToString(), true);
        }

        _healthBar?.Initialize(_stats.Health);
        _pickablesCatcher.Initialize(_stats.PickUpRange);
        _abilityInventory.Initialize();
        _coinInventory.Initialize();

        _upgrades = new List<Upgrade>();

        GetAbility(_stats.BaseWeapon);

        PlayerUpgrade currentUpgrade = _levelUpgrades.GetUpgrade(1);

        GetUpgrade(new Upgrade(currentUpgrade.DamageData));
        GetUpgrade(new Upgrade(currentUpgrade.HealthData));

        foreach (Upgrade upgrade in _levelContext.PlayerUpgrades)
        {
            GetUpgrade(upgrade);
        }
        
        foreach (Upgrade upgrade in customUpgrades)
        {
            GetUpgrade(upgrade);
        }

        _hpCanvas?.OnFixedUpdate();
    }

    private void Update()
    {
        Attack();
    }

    private void FixedUpdate()
    {
        OnFixedUpdate();
        _moveController.OnFixedUpdate();

        Vector3 pos = transform.position;
        _renderer.transform.LookAt(new Vector3(pos.x, pos.y + CameraDeltaPos.y, pos.z + CameraDeltaPos.z));

        _animator.SetBool(AnimatorBools.Walk.ToString(), isMoving);
        _moveDirectionArrow.enabled = isMoving;

        foreach (ProjectileWeapon weapon in _abilityInventory.ProjectileWeapons)
        {
            weapon.OnFixedUpdate();
        }
    }

    public override void Move(Vector3 direction)
    {
        Vector3 pos = transform.position;

        if (direction.x > 0 && _defaultViewSide.x < 0)
        {
            _renderer.flipX = true;
        }
        else if (direction.x < 0 && _defaultViewSide.x > 0)
        {
            _renderer.flipX = true;
        }
        else
        {
            _renderer.flipX = false;
        }

        transform.LookAt(pos + direction);
        transform.position = Vector3.MoveTowards(pos, pos + direction * _stats.Velocity.Value, _stats.Velocity.Value * Time.fixedDeltaTime);

        _hpCanvas?.OnFixedUpdate();
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
        _pickablesCatcher.UpdateRadius();

        for (int index = 0; index < _abilityInventory.Abilities.Count; index++)
        {
            _abilityInventory.Abilities[index].Upgrade(upgrade);
        }

        _upgrades.Add(upgrade);
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

    public override void DispelUpgrade(Upgrade upgrade)
    {
        base.DispelUpgrade(upgrade);

        foreach(AbilityContainer ability in _abilityInventory.Abilities)
        {
            ability.DispelUpgrade(upgrade);
        }
    }

    [ContextMenu("Die")]
    public override void Die()
    {
        base.Die();

        EventBus.Publish<IPlayerDieHandler>(handler => handler.OnPlayerDie());
    }

    protected enum AnimatorBools
    {
        Walk, 

        WithBlade,
        WithShotgun,
        WithPistol,
    }

    public class Factory: PlaceholderFactory<Player> { }
}
