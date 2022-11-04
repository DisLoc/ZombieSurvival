using UnityEngine;

public class PlayerSuperGun : PlayerGun
{
    protected override Vector3 GetProjectileMoveDirection()
    {
        return _targetDetector.GetDirectionToNearestTarget() + GetDeltaMoveDirection();
    }

    protected override Vector3 GetDeltaMoveDirection()
    {
        Vector3 delta = Vector3.zero;

        float randomScatter = Random.Range(-_scatterMultiplier, _scatterMultiplier);

        if (Random.Range(0, 2) % 2 == 0)
        {
            delta += transform.TransformDirection(Vector3.right) * randomScatter;
        }
        else
        {
            delta += transform.TransformDirection(Vector3.left) * randomScatter;
        }

        return delta;
    }
}
