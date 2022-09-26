using UnityEngine;

public class Projectile : ColliderWeapon, IPoolable
{
    private ObjectPool<Projectile> _pool;

    public void Initialize(ObjectPool<Projectile> pool)
    {
        _pool = pool;
    }

    public void ResetObject()
    {
        StopAllCoroutines();
    }

    public void Throw(Vector3 direction)
    {

    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.tag == TargetTag)
        {
            _pool.Release(this);
        }
    }
}
