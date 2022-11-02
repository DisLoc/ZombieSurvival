using UnityEngine;

public sealed class FieryBottleProjectile : Projectile
{
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private const float EPSILON = 0.125f;

    private float _trajectorySlope;

    public override void Initialize(ProjectileAbilityStats stats, ProjectileWeapon weapon)
    {
        base.Initialize(stats, weapon);
    }

    public override void Throw(Vector3 position)
    {
        base.Throw(position);

        _targetPosition = position;
        _startPosition = transform.position;

        UpdateMoveDirection();
    }

    protected override void Move()
    {
        if (transform.position.y - EPSILON <= 0f)
        {
            _releaseTimer = -1f;
            return;
        }

        base.Move();

        UpdateMoveDirection();
    }

    private void UpdateMoveDirection()
    {
        Vector3 position = transform.position;
        float firstHalf = (position - _startPosition).magnitude; // there must move up
        float secondHalf = (_targetPosition - position).magnitude; // and there down

        _trajectorySlope = secondHalf - firstHalf;

        _moveDirection = new Vector3(_targetPosition.x - position.x, _trajectorySlope, _targetPosition.z - position.z);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        // todo collision with ground
    }
}
