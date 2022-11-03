using UnityEngine;

public sealed class ShurikenProjectile : Projectile
{
    [SerializeField] private float _minSpeedMultiplier = 0.1f;
    [SerializeField] private float _maxSpeedMultiplier = 5f;

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

        _stopDuration = (weapon as PlayerShuriken).ProjectileStopDuration;
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

            if (_stopTimer <= 0f) _isStopped = false;
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
                _moveDirection = -_moveDirection;
                _moveForward = false;
                _isStopped = true;

                _stopTimer = _stopDuration.Value;
            }
        }
        else
        {
            _passedDistance += (newPosition - currentPosition).magnitude;
        }
    }
}
