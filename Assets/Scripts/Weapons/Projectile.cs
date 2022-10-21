using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolable, IFixedUpdatable
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    private Vector3 _moveDirection;

    private float _timer;
    private float _releaseDelay;

    private float _speed;
    private float _damage;

    private MonoPool<Projectile> _pool;

    public void ResetObject()
    {
        _pool = null;
        _moveDirection = Vector3.zero;
        _timer = 0;
        _releaseDelay = 0;
        _speed = 0;
        _damage = 0;

        StopAllCoroutines(); // test
    }

    public void Initialize(MonoPool<Projectile> pool, float releaseDelay, float speed, float damage)
    {
        _pool = pool;
        _releaseDelay = releaseDelay;
        _speed = speed;
        _damage = damage;
    }

    /// <summary>
    /// Throw projectile in direction
    /// </summary>
    /// <param name="direction">Direction need to move</param>
    public void Throw(Vector3 direction)
    {
        _moveDirection = direction.normalized;
        _timer = _releaseDelay;

        StartCoroutine(WaitRelease()); // test
        StartCoroutine(Move(direction)); // test
    }

    public IEnumerator Move(Vector3 direction) // test
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, _speed * Time.fixedDeltaTime);
        yield return new WaitForFixedUpdate();
        StartCoroutine(Move(direction));
    }

    private IEnumerator WaitRelease() // test
    {
        yield return new WaitForSeconds(_releaseDelay);

        if (_isDebug) Debug.Log(name + " released to pool");

        _pool.Release(this);
    }

    public void OnFixedUpdate()
    {
        Move();
        UpdateTimer();

        if (_timer <= 0f)
        {
            _pool.Release(this);
        }
    }

    /// <summary>
    /// Move into direction with current speed
    /// </summary>
    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + _moveDirection * _speed, _speed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Update release timer
    /// </summary>
    private void UpdateTimer()
    {
        _timer -= Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        DamageableObject obj = other.GetComponent<Zombie>();

        if (obj != null)
        {
            if (_isDebug) Debug.Log(name + " find target");

            obj.TakeDamage((int)_damage);
            _pool.Release(this);
        }
    }
}
