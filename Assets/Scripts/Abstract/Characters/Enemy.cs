using UnityEngine;

public abstract class Enemy : CharacterBase, IPoolable, IUpdatable
{
    [Header("Enemy settings")]
    [SerializeField] protected CapsuleCollider _selfCollider;
    [SerializeField] protected bool _hasExpReward = true;
    [SerializeField] protected CharacterStats _stats;

    protected Player _player; 
    protected MonoPool<Enemy> _pool;

    public CapsuleCollider Collider => _selfCollider;
    public bool HasExpReward => _hasExpReward;
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
    public virtual void Initialize(Player player, MonoPool<Enemy> pool = null)
    {
        _pool = pool;
        _player = player;

        _stats.Health.SetValue(_stats.Health.MaxHP);
        _healthBar.Initialize(_stats.Health);
    }

    public virtual void ResetObject()
    {
        _pool = null;

        if (_stats.BaseWeapon as ZombieCollider != null)
        {
            (_stats.BaseWeapon as ZombieCollider).OnReset();
        }
    }

    public virtual void OnUpdate()
    {
        Attack();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

        /*Move(new Vector3 
            (
                _player.transform.position.x - transform.position.x,
                0f,
                _player.transform.position.z - transform.position.z
            ));*/

        _hpCanvas?.OnFixedUpdate();
        
        Vector3 pos = transform.position;
        _renderer.transform.LookAt(new Vector3(pos.x, pos.y + _player.CameraDeltaPos.y, pos.z + _player.CameraDeltaPos.z));
    }

    public override void Move(Vector3 direction)
    {
        Vector3 pos = transform.position;

        if (direction.x > 0 && _defaultViewSide.x < 0)
        {
            _renderer.flipX = false;
        }
        else if (direction.x < 0 && _defaultViewSide.x > 0)
        {
            _renderer.flipX = false;
        }
        else
        {
            _renderer.flipX = true;
        }

        transform.LookAt(new Vector3(pos.x + direction.x, transform.position.y, pos.z + direction.z));
        transform.position = Vector3.MoveTowards(pos, pos + direction * _stats.Velocity.Value, _stats.Velocity.Value * Time.fixedDeltaTime);
    }

    protected override void Attack()
    {
        _stats.BaseWeapon.OnUpdate();
        _stats.BaseWeapon.Attack();
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

    public override void DispelUpgrade(Upgrade upgrade)
    {
        base.DispelUpgrade(upgrade);

        _stats.BaseWeapon.DispelUpgrade(upgrade);
    }
}
