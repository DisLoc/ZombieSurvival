using UnityEngine;

public class Projectile : MonoBehaviour, IPoolable, IFixedUpdatable
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    [Header("Collision settings")]
    [SerializeField] protected Tags _targetTag;

    [Header("Effects settings")]
    [SerializeField] protected ParticleSystem _particle;

    protected Vector3 _moveDirection;

    protected int _penetrations;
    protected float _releaseTimer;
    protected bool _onThrow;

    protected Duration _releaseDelay;
    protected ProjectileSpeed _speed;
    protected Damage _damage;
    protected PenetrationNumber _penetrationNumber;

    protected ProjectileWeapon _weapon;

    public virtual void ResetObject()
    {
        _moveDirection = Vector3.zero;

        transform.position = _weapon.transform.position;

        _penetrations = 0;
        _releaseTimer = 0;
        _onThrow = false;
        _releaseDelay = null;
        _speed = null;
        _damage = null;
        _penetrationNumber = null;

        transform.localScale = Vector3.one;
    }

    public virtual void Initialize(ProjectileAbilityStats stats, ProjectileWeapon weapon)
    {
        _releaseDelay = stats.ProjectileLifeDuration;
        _speed = stats.ProjectileSpeed;
        _damage = stats.Damage;
        _penetrationNumber = stats.PenetrationNumber;
        _weapon = weapon;

        if (_particle != null)
        {
            _particle.Stop();

            var main = _particle.main;

            main.startLifetime = _releaseDelay.Value;
            main.duration = _releaseDelay.Value;

            _particle.Play();
        }

        transform.localScale = new Vector3(stats.ProjectileSize.Value, stats.ProjectileSize.Value, stats.ProjectileSize.Value);
    }

    /// <summary>
    /// Throw projectile in direction
    /// </summary>
    /// <param name="direction">Direction need to move</param>
    public virtual void Throw(Vector3 direction)
    {
        transform.LookAt(transform.position + direction);

        _moveDirection = direction.normalized;
        _releaseTimer = _releaseDelay.Value;
        _onThrow = true;
    }

    public virtual void OnFixedUpdate()
    {
        if (_onThrow)
        {
            Move();
            UpdateTimer();
        }

        if (_releaseTimer <= 0f)
        {
            _weapon.OnProjectileRelease(this);
        }
    }

    /// <summary>
    /// Move into direction with current speed
    /// </summary>
    protected virtual void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + _moveDirection * _speed.Value, _speed.Value * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Update release timer based on fixedDeltaTime
    /// </summary>
    protected virtual void UpdateTimer()
    {
        _releaseTimer -= Time.fixedDeltaTime;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        DamageableObject obj = other.GetComponent<DamageableObject>();

        if (obj != null && obj.CompareTag(_targetTag.ToString()))
        {
            if (_isDebug) Debug.Log(name + " find target");

            obj.TakeDamage((int)_damage.Value);

            _penetrations++;
            
            if (!_penetrationNumber.ValueIsInfinite && _penetrations >= (int)_penetrationNumber.Value)
            {
                _weapon.OnProjectileRelease(this);
            }
        }
    }
}
