using UnityEngine;

public class ShellingProjectile : Projectile
{
    private const float EPSILON = 0.125f;

    protected override void Move()
    {
        base.Move();

        if (transform.position.y - EPSILON <= 0f)
        {
            _releaseTimer = -1f;
            return;
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        // todo collision with ground
    }
}
