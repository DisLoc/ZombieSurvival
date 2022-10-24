using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : CharacterBase
{
    [SerializeField] protected PlayerStats _stats;
    [SerializeField] protected ObjectCatcher _catcher;

    [Tooltip("Self collider")]
    [SerializeField] protected CapsuleCollider _collider;

    [Header("Animations settings")]
    [SerializeField] protected Animator _animator;
    /// <summary>
    /// Current animation bool
    /// </summary>
    [HideInInspector] public bool isMoving;

    [Header("Ability inventory settings")]
    [SerializeField] protected AbilityInventory _abilities;

    protected List<Upgrade> _upgrades;

    [Inject] protected LevelBuilder _levelBuilder;

    public override CharacterStats Stats => _stats;
    /// <summary>
    /// All abilities player getted in game
    /// </summary>
    public List<AbilityContainer> Abilities => _abilities.Abilities;

    public void Initialize()
    {
        transform.position = new Vector3(0, _levelBuilder.GridHeight + _collider.height * 0.5f, 0);

        _stats.Initialize();

        _healthBar.Initialize(_stats.Health);
        _catcher.Initialize(_stats.PickUpRange.Value);
        _abilities.Initialize();

        _upgrades = new List<Upgrade>();

        GetAbility(_stats.BaseWeapon);
    }

    private void Update() // test
    {
        Attack();

        _animator.SetBool("IsMoving", isMoving);
    }

    private void FixedUpdate()
    {
        foreach (ProjectileWeapon weapon in _abilities.ProjectileWeapons)
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
        foreach(Weapon weapon in _abilities.Weapons)
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

        _upgrades.Add(upgrade);

        for (int index = 0; index < _abilities.Abilities.Count; index++)
        {
            _abilities.Abilities[index].Upgrade(upgrade);
        }
    }

    /// <summary>
    /// Get new ability or upgrade existing
    /// </summary>
    /// <param name="ability"></param>
    public void GetAbility(AbilityContainer ability)
    {
        AbilityContainer abilityContainer = _abilities.Find(ability);

        if (abilityContainer != null)
        {
            if (abilityContainer.IsMaxLevel)
            {
                if (_isDebug) Debug.Log("This ability is max level!");

                return;
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
        }
        else
        {
            if (_isDebug) Debug.Log("Add new ability");

            AbilityContainer newAbility = _abilities.Add(ability);

            if (newAbility != null)
            {
                foreach (Upgrade upgrade in _upgrades)
                {
                    newAbility.Upgrade(upgrade);
                }

                GetUpgrade(newAbility.CurrentUpgrade.Upgrade);
            }
            else if (_isDebug) Debug.Log("Adding ability error!");
        }
    }
}
