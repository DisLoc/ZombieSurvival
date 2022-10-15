using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolable
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    private float _releaseDelay;
    private float _speed;
    private float _damage;

    private MonoPool<Projectile> _pool;

    public void ResetObject()
    {
        StopAllCoroutines();
    }

    public void Initialize(MonoPool<Projectile> pool, float releaseDelay, float speed, float damage)
    {
        _pool = pool;
        _releaseDelay = releaseDelay;
        _speed = speed;
        _damage = damage;
    }

    public void Throw(Vector3 direction)
    {
        StartCoroutine(WaitRelease());
        StartCoroutine(Move(direction));
    }

    public IEnumerator Move(Vector3 direction)
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, _speed * Time.fixedDeltaTime);
        yield return new WaitForFixedUpdate();
        StartCoroutine(Move(direction));
    }

    private IEnumerator WaitRelease()
    {
        yield return new WaitForSeconds(_releaseDelay);

        if (_isDebug) Debug.Log(name + " released to pool");

        _pool.Release(this);
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
