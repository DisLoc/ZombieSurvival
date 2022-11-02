using UnityEngine;

public class ShurikenProjectile : Projectile
{
    protected override void OnTriggerEnter(Collider other)
    {
        DamageableObject obj = other.GetComponent<DamageableObject>();

        if (obj != null && obj.CompareTag(_targetTag.ToString()))
        {
            if (_isDebug) Debug.Log(name + " find target");

            obj.TakeDamage((int)_damage.Value);
        }
    }
}
