using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : CharacterBase, IPoolable
{
    [Header("Enemy settings")]
    [SerializeField] protected CapsuleCollider _selfCollider;
    [SerializeField] protected bool _hasExpReward = true;
    [SerializeField] protected CharacterStats _stats;

    protected Player _player = null; 
    protected MonoPool<Enemy> _pool = null;

    public CapsuleCollider Collider => _selfCollider;
    public bool HasExpReward => _hasExpReward;
    public override CharacterStats Stats => _stats;

    protected virtual void Awake()
    {
        _stats.Initialize();
        _healthBar.Initialize(_stats.Health);

        _abilityInventory.Initialize();

        _upgrades = new List<Upgrade>();

        GetAbility(_stats.BaseWeapon);
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

        if (_abilityInventory.Weapons[0] as ZombieCollider != null)
        {
            if (_isDebug) Debug.Log("Reseting collider");

            (_abilityInventory.Weapons[0] as ZombieCollider).OnReset();
        }
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

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

    [ContextMenu("Die")]
    public override void Die()
    {
        base.Die();

        EventBus.Publish<IEnemyKilledHandler>(handler => handler.OnEnemyKilled(this));

        if (_pool != null)
        {
            if (_isDebug) Debug.Log(name + " returns to pool");
            
            _pool.Release(this);
        }
        else
        {
            if (_isDebug) Debug.Log("Destroy" + name);

            Destroy(gameObject);
        }
    }

    protected virtual void OnDisable()
    {
        EventBus.Publish<IObjectDisableHandler>(handler => handler?.OnObjectDisable(gameObject));
        
        if (_isDebug) Debug.Log(name + " disabled");
    }

    protected virtual void OnDestroy()
    {
        DestroyAbilities();
    }
}
