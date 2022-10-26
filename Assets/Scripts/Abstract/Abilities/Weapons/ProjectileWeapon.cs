using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileWeapon : Weapon, IFixedUpdatable
{
    [Header("Projectile settings")]
    [SerializeField] protected float _scatterMultiplier;
    [SerializeField] protected ProjectileAbilityStats _stats;

    protected MonoPool<Projectile> _pool;
    protected CleanupableList<Projectile> _projectiles;
    protected float _spawnIntervalTimer;
    protected bool _spawning;
    protected int _spawnCount;

    public override AbilityStats Stats => _stats;

    public override void Initialize()
    {
        base.Initialize();

        _pool = new MonoPool<Projectile>(_stats.Projectile, (int)_stats.ProjectileNumber.Value);
        _projectiles = new CleanupableList<Projectile>((int)_stats.ProjectileNumber.Value);


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
                for(int i = 0; i < _projectiles.List.Count; i++)
                {
                    Destroy(_projectiles[i].gameObject);
                    _projectiles.Remove(_projectiles[i], true);
                }

                _projectiles.Cleanup();
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
        _projectiles.Cleanup();

        for (int i = 0; i < _projectiles.List.Count; i++)
        {
            if (_projectiles[i] != null)
            {
                _projectiles[i].OnFixedUpdate();
            }
        }
    }

    protected virtual void SpawnProjectile()
    {
        Projectile projectile = _pool.PullDisabled();
        
        projectile.transform.position = transform.position;
        projectile.Initialize(_pool, _stats, this);
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

        _projectiles.Add(projectile);
    }

    protected abstract Vector3 GetProjectileMoveDirection();

    public void OnProjectileRelease(Projectile projectile)
    {
        _projectiles.Remove(projectile, true);
    }

    public override bool Upgrade(Upgrade upgrade)
    {
        _stats.GetUpgrade(upgrade);

        _targetDetector.UpdateRadius();

        return base.Upgrade(upgrade);
    }
}