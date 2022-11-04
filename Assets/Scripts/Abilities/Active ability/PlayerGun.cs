using UnityEngine;

public class PlayerGun : ProjectileWeapon
{
    protected override Vector3 GetProjectileMoveDirection()
    {
        return _targetDetector.GetDirectionToNearestTarget() + GetDeltaMoveDirection();
    }

    protected virtual Vector3 GetDeltaMoveDirection()
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
