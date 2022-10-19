using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterBase
{
    [SerializeField] protected PlayerStats _stats;
    [SerializeField] protected ObjectCatcher _catcher;

    [Header("Animations settings")]
    [SerializeField] protected Animator _animator;

    public bool isMoving;

    [Header("Ability inventory settings")]
    [SerializeField] protected AbilityInventory _abilities;

    protected List<Upgrade> _upgrades;

    public override CharacterStats Stats => _stats;
    public List<AbilityContainer> Abilities => _abilities.Abilities;

    [Header("Test")]
    public KeyCode shootKey;
    public Projectile projectile;
    private MonoPool<Projectile> _pool;

    public void Initialize()
    {
        _stats.Initialize();

        _healthBar.Initialize(_stats.HP);
        _catcher.Initialize(_stats.PickUpRange);
        _abilities.Initialize();

        _pool = new MonoPool<Projectile>(projectile, 10);
        _upgrades = new List<Upgrade>();

        GetAbility(_stats.BaseWeapon);
    }

    private void Update() // test
    {
        if (Input.GetKeyDown(shootKey))
        {
            Projectile p = _pool.Pull();
            p.transform.position = transform.position;

            p.Initialize(_pool, 5f, 5f, 100);
            p.Throw(transform.TransformDirection(Vector3.forward));
        }

        _animator.SetBool("IsMoving", isMoving);
    }

    public override void Move(Vector3 direction)
    {
        Vector3 pos = transform.position;

        transform.LookAt(pos + direction);
        transform.position = Vector3.MoveTowards(pos, pos + direction * _stats.Velocity, _stats.Velocity * Time.fixedDeltaTime);
    }

    public override void Attack()
    {
        foreach(Weapon weapon in _abilities.Weapons)
        {
            weapon.Attack();
        }
    }

    public override void GetUpgrade(Upgrade upgrade)
    {
        _upgrades.Add(upgrade);

        _stats.GetUpgrade(upgrade);

        for (int index = 0; index < _abilities.Abilities.Count; index++)
        {
            _abilities.Abilities[index].Upgrade(upgrade);
        }
    }

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
