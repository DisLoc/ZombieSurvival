using UnityEngine;

public class SuperShotgun : Shotgun
{
    protected override Vector3 GetDeltaMoveDirection()
    {
        Vector3 delta = transform.TransformDirection(Vector3.forward);


        switch (_spawnCount % (int)(_stats.ProjectileNumber.Value * 0.5f))
        {
            case 1:
                delta += transform.TransformDirection(Vector3.right) * _scatterMultiplier;
                break;
            case 2:
                delta += transform.TransformDirection(Vector3.left) * _scatterMultiplier;
                break;

            default: break;
        }

        return _spawnCount >= (int)(_stats.ProjectileNumber.Value * 0.5f) ? -delta : delta;
    }
}
