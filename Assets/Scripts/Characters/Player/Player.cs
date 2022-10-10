using System.Collections.Generic;
using UnityEngine;

public sealed class Player : CharacterBase
{
    [SerializeField] private PlayerStats _stats;
    [SerializeField] private ExpLevel _expLevel;
    [SerializeField] private ObjectCatcher _catcher;

    public override CharacterStats Stats => _stats;

    private AbilityInventory _abilities;

    [Header("Test")]
    public PassiveAbilityData moveSpeedAbility;
    public KeyCode shootKey;
    public Projectile projectile;
    private MonoPool<Projectile> _pool;

    public void Initialize()
    {
        _stats.Initialize();

        _catcher.Initialize(_stats.PickUpRange);
        _healthBar.Initialize(_stats.HP);

        _pool = new MonoPool<Projectile>(projectile, 10);
        _abilities = new AbilityInventory();
    }

    private void Update() // test
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            GetUpgrade(moveSpeedAbility.PassiveAbility.CurrentUpgrade.Upgrade);
        }

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
        throw new System.NotImplementedException();
    }

    public override void GetUpgrade(Upgrade upgrade)
    {
        _stats.GetUpgrade(upgrade);
    }

    public void GetAbility(Ability ability)
    {
        _abilities.Add(ability);
    }
}
