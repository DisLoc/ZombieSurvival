using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolable, IFixedUpdatable
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    protected Vector3 _moveDirection;

    protected int _penetrations;
    protected float _releaseTimer;
    protected bool _onThrow;

    protected Duration _releaseDelay;
    protected ProjectileSpeed _speed;
    protected Damage _damage;
    protected PenetrationNumber _penetrationNumber;

    protected MonoPool<Projectile> _pool;
    protected ProjectileWeapon _weapon;

    public void ResetObject()
    {
        _pool = null;
        _moveDirection = Vector3.zero;

        transform.position = _weapon.transform.position;

        _penetrations = 0;
        _releaseTimer = 0;
        _onThrow = false;
        _releaseDelay = null;
        _speed = null;
        _damage = null;
        _penetrationNumber = null;
    }

    public void Initialize(MonoPool<Projectile> pool, ProjectileAbilityStats stats, ProjectileWeapon weapon)
    {
        _pool = pool;
        _releaseDelay = stats.ProjectileLifeDuration;
        _speed = stats.ProjectileSpeed;
        _damage = stats.Damage;
        _penetrationNumber = stats.PenetrationNumber;
        _weapon = weapon;
    }

    /// <summary>
    /// Throw projectile in direction
    /// </summary>
    /// <param name="direction">Direction need to move</param>
    public void Throw(Vector3 direction)
    {
        transform.LookAt(transform.position + direction);

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

            _penetrations++;
            
            if (!_penetrationNumber.ValueIsInfinite && _penetrations >= (int)_penetrationNumber.Value)
            {
                _weapon.OnProjectileRelease(this);
                _pool.Release(this);
            }
        }
    }
}
