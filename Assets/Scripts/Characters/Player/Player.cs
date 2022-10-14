using System.Collections.Generic;
using UnityEngine;

public sealed class Player : CharacterBase
{
    [SerializeField] private PlayerStats _stats;
    [SerializeField] private ExpLevel _expLevel;
    [SerializeField] private ObjectCatcher _catcher;

    private AbilityInventory _abilities;
    private List<Upgrade> _upgrades;

    public override CharacterStats Stats => _stats;

    [Header("Test")]
    public KeyCode shootKey;
    public Projectile projectile;
    private MonoPool<Projectile> _pool;

    public void Initialize()
    {
        _stats.Initialize();

        _catcher.Initialize(_stats.PickUpRange);
        _healthBar.Initialize(_stats.HP);

        _pool = new MonoPool<Projectile>(projectile, 10);
        _abilities = new AbilityInventory(transform);
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
    }

    public override void Move(Vector3 direction)
    {
        Vector3 pos = transform.position;

        transform.LookAt(pos + direction);
        transform.position = Vector3.MoveTowards(pos, pos + direction * _stats.Velocity, _stats.Velocity * Time.fixedDeltaTime);
    }

    public override void Attack()
    {
        _stats.Weapon.Attack();

        foreach(Weapon weapon in _abilities.Weapons)
        {
            weapon.Attack();
        }
    }

    public override void GetUpgrade(Upgrade upgrade)
    {
        _upgrades.Add(upgrade);

        _stats.GetUpgrade(upgrade);

        /*
        foreach(AbilityData ability in _abilities.Abilities)
        {
            ability.Upgrade(upgrade);
        }
        */
    }

    public void GetAbility(AbilityData ability)
    {
        /*
        if (!_abilities.Abilities.Contains(ability))
        {
            foreach (Upgrade upgrade in _upgrades)
            {
                ability.Upgrade(upgrade);
            }

            _abilities.Add(ability);
        }

        GetUpgrade(ability.CurrentUpgrade.Upgrade);
        */
    }
}
