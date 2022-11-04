using UnityEngine;

public class PlayerFieryBottle : ProjectileWeapon
{
    [Header("Fiery bottle settings")]
    [SerializeField] protected FirePuddle _puddlePrefab;
    [SerializeField] protected bool _destroyPuddlesOnAttack;
    [SerializeField] protected Radius _puddleRadius;
    [SerializeField] protected Duration _puddleLifeDuration;
    [SerializeField] protected Cooldown _puddleAttackInterval;

    protected ObjectSpawner<FirePuddle> _puddlesSpawner;

    public Radius PuddleRadius => _puddleRadius;
    public Duration PuddleLifeDuration => _puddleLifeDuration;
    public Cooldown PuddleAttackInterval => _puddleAttackInterval;

    public override void Initialize()
    {
        base.Initialize();

        _puddleRadius.Initialize();
        _puddleLifeDuration.Initialize();
        _puddleAttackInterval.Initialize();

        _puddlesSpawner = new ObjectSpawner<FirePuddle>(_puddlePrefab, (int)_stats.ProjectileNumber.Value);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        for(int i = 0; i < _puddlesSpawner.SpawnedObjects.Count; i++)
        {
            _puddlesSpawner.SpawnedObjects[i]?.OnUpdate();
        }

        _puddlesSpawner.SpawnedObjects.Cleanup();
    }

    public override void Attack()
    {
        if (_isReady && _destroyPuddlesOnAttack)
        {
            if (_puddlesSpawner.SpawnCount > 0)
            {
                for (int i = 0; i < _puddlesSpawner.SpawnedObjects.Count; i++)
                {
                    _puddlesSpawner.Release(_puddlesSpawner.SpawnedObjects[i]);
                }

                _puddlesSpawner.SpawnedObjects.Cleanup();
            }
        }

        base.Attack();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Return target position instead of move direction</returns>
    protected override Vector3 GetProjectileMoveDirection()
    {
        Vector3 pos = transform.position;

        return new Vector3
            (
                Random.Range(pos.x - _stats.AttackRange.Value - _scatterMultiplier, pos.x + _stats.AttackRange.Value + _scatterMultiplier),
                transform.position.y - 1f,
                Random.Range(pos.z -_stats.AttackRange.Value - _scatterMultiplier, pos.z + _stats.AttackRange.Value + _scatterMultiplier)
            );
    }

    public override void OnProjectileRelease(Projectile projectile)
    {
        Vector3 pos = projectile.transform.position;
        FirePuddle puddle = _puddlesSpawner.Spawn(new Vector3(pos.x, 0.05f, pos.z));
        puddle.Initialize(_stats, this);

        base.OnProjectileRelease(projectile);
    }

    public void OnPuddleRelease(FirePuddle puddle)
    {
        _puddlesSpawner.Release(puddle);
    }

    public override bool Upgrade(Upgrade upgrade)
    {
        _puddleAttackInterval.Upgrade(upgrade);
        _puddleLifeDuration.Upgrade(upgrade);
        _puddleRadius.Upgrade(upgrade);

        return base.Upgrade(upgrade);
    }
}
