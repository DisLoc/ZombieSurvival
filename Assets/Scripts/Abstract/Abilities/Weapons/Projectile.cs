using UnityEngine;

public class Projectile : MonoBehaviour, IPoolable, IFixedUpdatable
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    [Header("Collision settings")]
    [SerializeField] protected Tags _targetTag;

    [Header("Effects settings")]
    [SerializeField] protected ParticleSystem _particle;

    [Header("Throw settings")]
    [SerializeField] protected bool _destroyOnExitAttackRange;

    protected Vector3 _moveDirection;

    protected float _passedDistance;
    protected int _penetrations;
    protected float _releaseTimer;
    protected bool _onThrow;

    protected Duration _releaseDelay;
    protected ProjectileSpeed _speed;
    protected Damage _damage;
    protected PenetrationNumber _penetrationNumber;
    protected Radius _attackRange;

    protected ProjectileWeapon _weapon;

    public virtual void ResetObject()
    {
        _moveDirection = Vector3.zero;

        transform.position = _weapon.transform.position;

        _passedDistance = 0;
        _penetrations = 0;
        _releaseTimer = 0;
        _onThrow = false;
        _releaseDelay = null;
        _speed = null;
        _damage = null;
        _penetrationNumber = null;
        _attackRange = null;
        _weapon = null;
    }

    public virtual void Initialize(ProjectileAbilityStats stats, ProjectileWeapon weapon)
    {
        _releaseDelay = stats.ProjectileLifeDuration;
        _speed = stats.ProjectileSpeed;
        _damage = stats.Damage;
        _penetrationNumber = stats.PenetrationNumber;
        _attackRange = stats.AttackRange;

        _weapon = weapon;

        if (_particle != null)
        {
            _particle.Stop();

            var main = _particle.main;

            main.startLifetime = _releaseDelay.Value;
            main.duration = _releaseDelay.Value;

            _particle.transform.localScale = new Vector3(stats.ProjectileSize.Value, stats.ProjectileSize.Value, stats.ProjectileSize.Value);
            _particle.Play();
        }
        else if (_isDebug) Debug.Log("Missing particle");

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
        _passedDistance = 0;
        _onThrow = true;
    }

    public virtual void OnFixedUpdate()
    {
        if (_onThrow)
        {
            Move();
            UpdateTimer();
        }

        if (_destroyOnExitAttackRange && _passedDistance >= _attackRange.Value)
        {
            _releaseTimer = -1f;
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
        Vector3 currentPosition = transform.position;
        Vector3 newPosition = Vector3.MoveTowards(currentPosition, currentPosition + _moveDirection * _speed.Value, _speed.Value * Time.fixedDeltaTime);

        transform.position = newPosition;

        _passedDistance += (newPosition - currentPosition).magnitude;
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
