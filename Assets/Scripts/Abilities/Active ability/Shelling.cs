using UnityEngine;

public class Shelling : ProjectileWeapon
{
    [Header("Shelling ability settings")]
    [SerializeField] protected float _projectileSpawnHeight;
    [SerializeField] protected ShellingExplosion _explosionPrefab;

    [SerializeField] protected bool _destroyExplosionsOnAttack;
    [SerializeField] protected Radius _explosionRadius;
    [SerializeField] protected Duration _explosionLifeDuration;

    protected ObjectSpawner<ShellingExplosion> _explosionsSpawner;

    public Radius ExplosionRadius => _explosionRadius;
    public Duration ExplosionLifeDuration => _explosionLifeDuration;

    public float ProjectileSpawnHeight => _projectileSpawnHeight;

    public override void Initialize()
    {
        base.Initialize();

        _explosionRadius.Initialize();
        _explosionLifeDuration.Initialize();

        _explosionsSpawner = new ObjectSpawner<ShellingExplosion>(_explosionPrefab, (int)_stats.ProjectileNumber.Value);
    }

    public override bool Upgrade(Upgrade upgrade)
    {
        _explosionRadius.Upgrade(upgrade);
        _explosionLifeDuration.Upgrade(upgrade);

        return base.Upgrade(upgrade);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        for (int i = 0; i < _explosionsSpawner.SpawnedObjects.Count; i++)
        {
            _explosionsSpawner.SpawnedObjects[i]?.OnUpdate();
        }

        _explosionsSpawner.SpawnedObjects.Cleanup();
    }

    public override void Attack()
    {
        if (_isReady && _destroyExplosionsOnAttack)
        {
            if (_explosionsSpawner.SpawnCount > 0)
            {
                for (int i = 0; i < _explosionsSpawner.SpawnedObjects.Count; i++)
                {
                    _explosionsSpawner.Release(_explosionsSpawner.SpawnedObjects[i]);
                }

                _explosionsSpawner.SpawnedObjects.Cleanup();
            }
        }

        base.Attack();
    }

    protected override Vector3 GetProjectileMoveDirection()
    {
        return Vector3.down + Vector3.back;
    }

    protected override Vector3 GetProjectilePosition()
    {
        return _targetDetector.GetRandomTargetPosition() + Vector3.up * _projectileSpawnHeight + Vector3.forward * _projectileSpawnHeight + GetDeltaMoveDirection();
    }

    public override void OnProjectileRelease(Projectile projectile)
    {
        Vector3 pos = projectile.transform.position;
        ShellingExplosion explosion = _explosionsSpawner.Spawn(new Vector3(pos.x, 0.05f, pos.z));
        explosion.Initialize(_stats, this);

        base.OnProjectileRelease(projectile);
    }

    public void OnExplosionRelease(ShellingExplosion explosion)
    {
        _explosionsSpawner.Release(explosion);
    }
}
