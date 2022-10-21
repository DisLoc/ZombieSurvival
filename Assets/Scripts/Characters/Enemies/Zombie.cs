using UnityEngine;
using Zenject;

public class Zombie : CharacterBase, IPoolable
{
    [SerializeField] protected CharacterStats _stats;

    [Inject] protected Transform _player; 
    // use factory for injection (end of script)
    // example in ExpCrystal, CrystalSpawner and CrystalFactoryInstaller
    // also use FactoryMonoPool for spawning
    protected FactoryMonoPool<Zombie, Factory> _pool;

    public override CharacterStats Stats => _stats;

    void Start()
    {
        // player automatically injected 
        //_player = GameObject.FindGameObjectWithTag("Player").gameObject.transform; (not needed)

        _stats.Initialize();
        _healthBar.Initialize(_stats.Health);

        _stats.BaseWeapon.Initialize(); // there will be ability inventory with weapons for bosses
        // they have collider and projectile weapons
        // or make ZombieBossStats : CharacterStats
        // and add more weapons instead of ability inventory
    }

    // Need initialize every time when spawn
    public void Initialize(FactoryMonoPool<Zombie, Factory> pool)
    {
        _pool = pool;
    }

    public void ResetObject()
    {
        _pool = null;
    }

    void Update()
    {
        Move(new Vector3(0, 0, 0));
    }

    public override void Die()
    {
        base.Die();

        EventBus.Publish<IEnemyKilledHandler>(handler => handler.OnEnemyKilled(this));

        // not needed destroy with pool
        //Destroy(gameObject);

        _pool.Release(this);
    }

    public override void Move(Vector3 direction)
    {
        transform.position = Vector3.MoveTowards(transform.position,  _player.transform.position, _stats.Velocity * Time.deltaTime);
        transform.LookAt(_player);
    }

    public override void Attack()
    {
        _stats.BaseWeapon.Attack();
    }

    // Zenject factory for auto injection fields (player)
    public class Factory : PlaceholderFactory<Zombie> { }
}
