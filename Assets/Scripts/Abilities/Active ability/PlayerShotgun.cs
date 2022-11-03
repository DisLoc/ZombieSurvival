using UnityEngine;

public sealed class PlayerShotgun : ProjectileWeapon
{
    protected override Vector3 GetProjectileMoveDirection()
    {
        return GetDeltaMoveDirection();
    }

    private Vector3 GetDeltaMoveDirection()
    {
        Vector3 delta = transform.TransformDirection(Vector3.forward);

        if (_spawnCount % 2 == 0)
        {
            delta += transform.TransformDirection(Vector3.right) * _scatterMultiplier;
        }
        else
        {
            delta += transform.TransformDirection(Vector3.left) * _scatterMultiplier;
        }

        if (_spawnCount >= _stats.ProjectileNumber.Value * 0.5f)
        {
            delta = -delta;
        }

        return delta;
    }
}
