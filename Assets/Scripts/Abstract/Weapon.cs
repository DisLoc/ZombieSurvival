using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Projectile _projectilePrefab;
    [SerializeField] protected ProjectileNumber _projectileNumber;
    [SerializeField] protected ProjectileSpeed _projectileSpeed;
}
