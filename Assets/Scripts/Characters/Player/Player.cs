using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterBase
{
    [SerializeField] protected PlayerStats _stats;
    [SerializeField] protected PlayerExpLevel _expLevel;
    [SerializeField] protected ObjectCatcher _catcher;

    [Header("Ability inventory settings")]
    [SerializeField] protected AbilityInventory _abilities;

    protected List<Upgrade> _upgrades;

    public override CharacterStats Stats => _stats;

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

        foreach (AbilityContainer ability in _abilities.Abilities)
        {
            ability.Upgrade(upgrade);
        }
    }

    public void GetAbility(AbilityContainer ability)
    {
        if (ability as PassiveAbility != null)
        {
            GetUpgrade(ability.CurrentUpgrade.Upgrade);
        }

        _abilities.Add(ability);

        foreach(Upgrade upgrade in _upgrades)
        {
            ability.Upgrade(upgrade);
        }
    }
}
