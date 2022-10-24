using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolable, IFixedUpdatable
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    protected Vector3 _moveDirection;

    protected float _releaseTimer;
    protected bool _onThrow;

    protected Duration _releaseDelay;
    protected ProjectileSpeed _speed;
    protected Damage _damage;

    protected MonoPool<Projectile> _pool;
    protected ProjectileWeapon _weapon;

    public void ResetObject()
    {
        _pool = null;
        _moveDirection = Vector3.zero;

        _releaseTimer = 0;
        _onThrow = false;
        _releaseDelay = null;
        _speed = null;
        _damage = null;
    }

    public void Initialize(MonoPool<Projectile> pool, Duration releaseDelay, ProjectileSpeed speed, Damage damage, ProjectileWeapon weapon)
    {
        _pool = pool;
        _releaseDelay = releaseDelay;
        _speed = speed;
        _damage = damage;
        _weapon = weapon;
    }

    /// <summary>
    /// Throw projectile in direction
    /// </summary>
    /// <param name="direction">Direction need to move</param>
    public void Throw(Vector3 direction)
    {
        _moveDirection = direction.normalized;
        _releaseTimer = _releaseDelay.Value;
        _onThrow = true;
    }

    public void OnFixedUpdate()
    {
        if (_onThrow)
        {
            Move();
            UpdateTimer();
        }

        if (_releaseTimer <= 0f)
        {
            _weapon.OnProjectileRelease(this);
            _pool.Release(this);
        }
    }

    /// <summary>
    /// Move into direction with current speed
    /// </summary>
    protected void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + _moveDirection * _speed.Value, _speed.Value * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Update release timer
    /// </summary>
    private void UpdateTimer()
    {
        _releaseTimer -= Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        DamageableObject obj = other.GetComponent<Zombie>();

        if (obj != null)
        {
            if (_isDebug) Debug.Log(name + " find target");

            obj.TakeDamage((int)_damage.Value);
            _weapon.OnProjectileRelease(this);
            _pool.Release(this);
        }
    }
}
