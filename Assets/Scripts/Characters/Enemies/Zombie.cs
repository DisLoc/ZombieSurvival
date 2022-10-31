using UnityEngine;
using Zenject;

public class Zombie : CharacterBase, IPoolable, IUpdatable
{
    [SerializeField] protected CharacterStats _stats;

    protected Player _player; 
    // use factory for injection (end of script)
    // example in ExpCrystal, CrystalSpawner and CrystalFactoryInstaller
    // also use FactoryMonoPool for spawning
    protected MonoPool<Zombie> _pool;

    public override CharacterStats Stats => _stats;

    protected void Awake()
    {
        _stats.Initialize();
        _healthBar.Initialize(_stats.Health);

        _stats.BaseWeapon.Initialize(); // there will be ability inventory with weapons for bosses
    }

    /// <summary>
    /// Need initialization every time when pull object
    /// </summary>
    /// <param name="pool"></param>
    public void Initialize(Player player, MonoPool<Zombie> pool = null)
    {
        _pool = pool;
        _player = player;

        _stats.Health.SetValue(_stats.Health.MaxHP);
        _healthBar.Initialize(_stats.Health);
    }

    public void ResetObject()
    {
        _pool = null;

        if (_stats.BaseWeapon as ZombieCollider != null)
        {
            (_stats.BaseWeapon as ZombieCollider).OnReset();
        }
    }

    public void OnUpdate()
    {
        Attack();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

        Move(_player.transform.position - transform.position);
    }

    public override void Die()
    {
        base.Die();

        EventBus.Publish<IEnemyKilledHandler>(handler => handler.OnEnemyKilled(this));

        if (_pool != null)
        {
            _pool.Release(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public override void Move(Vector3 direction)
    {
        Vector3 pos = transform.position;

        transform.LookAt(new Vector3(pos.x + direction.x, transform.position.y, pos.z + direction.z));
        transform.position = Vector3.MoveTowards(pos, pos + direction * _stats.Velocity.Value, _stats.Velocity.Value * Time.fixedDeltaTime);
    }

    protected override void Attack()
    {
        _stats.BaseWeapon.OnUpdate();
        _stats.BaseWeapon.Attack();
    }

    // Zenject factory for auto injection fields (player)
    public class Factory : PlaceholderFactory<Zombie> { }
}
