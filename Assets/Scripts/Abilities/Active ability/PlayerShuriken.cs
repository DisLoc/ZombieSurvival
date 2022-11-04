using UnityEngine;

public class PlayerShuriken : ProjectileWeapon
{
    [SerializeField] protected Duration _projectileStopDuration;

    public Duration ProjectileStopDuration => _projectileStopDuration;

    public override void Initialize()
    {
        base.Initialize();

        _projectileStopDuration.Initialize();
    }

    public override bool Upgrade(Upgrade upgrade)
    {
        _projectileStopDuration.Upgrade(upgrade);

        return base.Upgrade(upgrade);
    }

    protected override Vector3 GetProjectileMoveDirection()
    {
        return _targetDetector.GetDirectionToNearestTarget() + GetDeltaMoveDirection();
    }

    protected Vector3 GetDeltaMoveDirection()
    {
        Vector3 delta = Vector3.zero;

        if (_spawnCount == 0) return delta;

        else if (_spawnCount % 2 == 0)
        {
            delta += transform.TransformDirection(Vector3.right) * _scatterMultiplier * _spawnCount;
        }
        else
        {
            delta += transform.TransformDirection(Vector3.left) * _scatterMultiplier * _spawnCount;
        }

        return delta;
    }
}
