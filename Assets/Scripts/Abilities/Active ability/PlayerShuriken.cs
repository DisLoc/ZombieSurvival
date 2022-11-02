using UnityEngine;

public sealed class PlayerShuriken : ProjectileWeapon
{
    protected override Vector3 GetProjectileMoveDirection()
    {
        return _targetDetector.GetDirectionToNearestTarget();
    }
}
