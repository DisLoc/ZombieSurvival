using UnityEngine;

public sealed class FieryBottleProjectile : Projectile
{
    [SerializeField] private ParticleSystem _sparkParticle;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _trajectorySlope;

    private const float EPSILON = 0.125f;

    public override void Initialize(ProjectileAbilityStats stats, ProjectileWeapon weapon, TagList tags = null)
    {
        base.Initialize(stats, weapon, tags);

        if (_sparkParticle != null)
        {
            _sparkParticle.Stop();

            var main = _sparkParticle.main;

            main.startLifetime = _releaseDelay.Value;
            main.duration = _releaseDelay.Value;

            _sparkParticle.transform.localScale = new Vector3(stats.ProjectileSize.Value, stats.ProjectileSize.Value, stats.ProjectileSize.Value);
            _sparkParticle.Play();
        }
        else if (_isDebug) Debug.Log("Missing spark particle");
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
