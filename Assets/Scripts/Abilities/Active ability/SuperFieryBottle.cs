using UnityEngine;

public class SuperFieryBottle : FieryBottle
{
    protected override Vector3 GetProjectileMoveDirection()
    {
        return transform.position + GetDeltaMoveDirection() * _stats.AttackRange.Value;
    }

    protected override Vector3 GetDeltaMoveDirection()
    {
        float delta = 2 * Mathf.PI / _stats.ProjectileNumber.Value;

        return new Vector3
            (
                Mathf.Cos(delta * _spawnCount) * _scatterMultiplier,
                0,
                Mathf.Sin(delta * _spawnCount) * _scatterMultiplier
            ).normalized;
    }
}
