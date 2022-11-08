using UnityEngine;

public sealed class ShurikenProjectile : Projectile
{
    [SerializeField] private ParticleSystem _sparkParticle;

    [Space(5)]
    [SerializeField][Range(0.01f, 1f)] private float _minSpeedMultiplier = 0.1f;
    [SerializeField][Range(1f, 5f)] private float _maxSpeedMultiplier = 5f;

    [SerializeField][Range(3f, 10f)] private float _exitRangeMultiplier = 3f;

    private Vector3 _startPosition;
    private Duration _stopDuration;

    private float _stopTimer;
    private float _speedMultiplier;

    private bool _moveForward;
    private bool _isStopped;

    private const float MAX_DELTA_POS = 0.15f;

    public override void ResetObject()
    {
        base.ResetObject();

        _startPosition = Vector3.zero;
        _speedMultiplier = 0;
        _moveForward = false;
    }

    public override void Initialize(ProjectileAbilityStats stats, ProjectileWeapon weapon)
    {
        base.Initialize(stats, weapon);

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

        _stopDuration = (weapon as Shuriken).ProjectileStopDuration;
    }

    public override void Throw(Vector3 direction)
    {
        base.Throw(direction);

        _startPosition = transform.position;
        _moveForward = true;
    }

    protected override void UpdateTimer()
    {
        base.UpdateTimer();
        
        if (_isStopped)
        {
            _stopTimer -= Time.fixedDeltaTime;

            if (_stopTimer <= 0f)
            {
                _isStopped = false;

                _moveForward = false;
                _moveDirection = -_moveDirection;
            }
        }
        
    }

    protected override void Move()
    {
        if (_isStopped) return;

        if (_moveForward)
        {
            _speedMultiplier = _attackRange.Value - _passedDistance;

            if (_speedMultiplier > 1f && _moveForward)
            {
                _speedMultiplier = 1f;
            }
            else if (_speedMultiplier < 0f && _moveForward)
            {
                _speedMultiplier = 0f;
            }
        }
        else if (!_moveForward)
        {
            _speedMultiplier = Mathf.Abs(_attackRange.Value - _passedDistance);

            if (_speedMultiplier < _minSpeedMultiplier)
            {
                _speedMultiplier = _minSpeedMultiplier;
            }
            else if (_speedMultiplier > _maxSpeedMultiplier)
            {
                _speedMultiplier = _maxSpeedMultiplier;
            }
        }

        Vector3 currentPosition = transform.position;
        Vector3 newPosition = Vector3.MoveTowards(currentPosition, 
                                                  currentPosition + _moveDirection * _speed.Value * _speedMultiplier, 
                                                  _speed.Value * _speedMultiplier * Time.fixedDeltaTime);

        transform.position = newPosition;

        if (_moveForward)
        {
            _passedDistance = (newPosition - _startPosition).magnitude;

            if (_passedDistance + MAX_DELTA_POS >= _attackRange.Value)
            {
                _isStopped = true;

                _stopTimer = _stopDuration.Value;
            }
        }
        else
        {
            _passedDistance += (newPosition - currentPosition).magnitude;

            if (_passedDistance > _attackRange.Value * _exitRangeMultiplier)
            {
                _releaseTimer = -1f;
            }
        }
    }
}
