using UnityEngine;

public class ColliderWeapon : Weapon
{
    [SerializeField] protected Collider _attackCollider;

    public override void Initialize()
    {
        base.Initialize();

        _attackCollider.isTrigger = true;
        _isReady = true;
    }

    public override void Attack()
    {
        if (_isReady)
        {
            if (_target == null)
            {
                if (_isDebug) Debug.Log("Target is missing!");

                return;
            }

            _target.TakeDamage(Damage);

            StartCoroutine(WaitReload());
        }
        else return;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == TargetTag)
        {
            if (_isDebug) Debug.Log("Collides with target");

            _target = other.GetComponent<DamageableObject>();

            Attack();
        }
    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.tag == TargetTag)
        {
            Attack();
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.tag == TargetTag)
        {
            if (_isDebug) Debug.Log("Target exit");

            _target = null;
        }
    }
}
