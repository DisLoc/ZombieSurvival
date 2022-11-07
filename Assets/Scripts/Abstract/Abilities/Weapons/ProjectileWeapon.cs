using UnityEngine;

public abstract class ProjectileWeapon : Weapon, IFixedUpdatable
{
    [Header("Projectile settings")]
    [SerializeField][Range(0, 2)] protected float _scatterMultiplier;
    [SerializeField] protected ProjectileAbilityStats _stats;

    protected ObjectSpawner<Projectile> _projectilePool;

    protected float _spawnIntervalTimer;
    protected bool _spawning;
    protected int _spawnCount;

    public override AbilityStats Stats => _stats;

    public override void Initialize()
    {
        base.Initialize();

        _projectilePool = new ObjectSpawner<Projectile>(_stats.Projectile, (int)_stats.ProjectileNumber.Value);

        _spawnCount = 0;
        _spawnIntervalTimer = _stats.ProjectilesSpawnInterval.Value;
        _spawning = false;
    }

    public override void Attack()
    {
        if (_isReady)
        {
            base.Attack();

            if (_stats.DestroyProjectilesOnAttack)
            {
                _projectilePool.SpawnedObjects.Cleanup();

                for (int i = 0; i < _projectilePool.SpawnCount; i++)
                {
                    _projectilePool.Release(_projectilePool.SpawnedObjects[i]);
                }

                _projectilePool.SpawnedObjects.Cleanup();
            }

            _isReady = false;
            _spawning = true;
            _spawnCount = 0;
            _spawnIntervalTimer = _stats.ProjectilesSpawnInterval.Value;
        }
    }

    public override void OnUpdate()
    {
        if (_spawning)
        {
            if (_spawnIntervalTimer <= 0f)
            {
                SpawnProjectile();
            }
            else
            {
                _spawnIntervalTimer -= Time.deltaTime;
            }
        }

        else base.OnUpdate();
    }

    public virtual void OnFixedUpdate()
    {
        for (int i = 0; i < _projectilePool.SpawnCount; i++)
        {
            _projectilePool.SpawnedObjects[i]?.OnFixedUpdate();
        }

        _projectilePool.SpawnedObjects.Cleanup();
    }

    protected virtual void SpawnProjectile()
    {
        Projectile projectile = _projectilePool.SpawnDisabled(GetProjectilePosition());
        
        projectile.Initialize(_stats, this);
        projectile.Throw(GetProjectileMoveDirection());
        projectile.gameObject.SetActive(true);

        _spawnIntervalTimer = _stats.ProjectilesSpawnInterval.Value;
        _spawnCount++;
        
        if (_spawnCount >= (int)_stats.ProjectileNumber.Value)
        {
            _spawning = false;
            _attackIntervalTimer = _stats.AttackInterval.Value;
            _spawnCount = 0;
        }
    }

    protected virtual Vector3 GetProjectilePosition()
    {
        return transform.position;
    }

    protected virtual Vector3 GetProjectileMoveDirection()
    {
        return transform.TransformDirection(Vector3.forward) + GetDeltaMoveDirection();
    }

    protected virtual Vector3 GetDeltaMoveDirection()
    {
        return new Vector3
            (
                Random.Range(-_scatterMultiplier, _scatterMultiplier),
                0f,
                Random.Range(-_scatterMultiplier, _scatterMultiplier)
            );
    }

    public virtual void OnProjectileRelease(Projectile projectile)
    {
        _projectilePool.Release(projectile);
    }

    public override bool Upgrade(Upgrade upgrade)
    {
        _stats.GetUpgrade(upgrade);

        _targetDetector.UpdateRadius();

        return base.Upgrade(upgrade);
    }

    public override void DestroyAbility()
    {
        _projectilePool.ClearPool();

        base.DestroyAbility();
    }
}