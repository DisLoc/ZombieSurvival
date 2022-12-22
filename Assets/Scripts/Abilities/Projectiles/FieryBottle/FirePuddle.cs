using System.Collections.Generic;
using UnityEngine;

public sealed class FirePuddle : MonoBehaviour, IPoolable, IUpdatable
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private TargetDetector _targetDetector;
    //[SerializeField] private SoundList _sounds;

    private FieryBottle _weapon;

    private float _releaseTimer;
    private float _attackTimer;

    private Damage _damage;
    private Cooldown _attackInterval;

    public void ResetObject()
    {
        _weapon = null;
        _damage = null;
        _attackInterval = null;
        _attackTimer = 0;
        _releaseTimer = 0;
    }

    public void Initialize(ProjectileAbilityStats stats, FieryBottle weapon, TagList targetTags)
    {
        if (targetTags != null)
        {
            _targetDetector.SetTargetTags(targetTags);
        }

        _weapon = weapon;
        _targetDetector.Initialize(_weapon.PuddleRadius);

        _releaseTimer = _weapon.PuddleLifeDuration.Value;
        _damage = stats.Damage;
        _attackInterval = _weapon.PuddleAttackInterval;
        _attackTimer = 0;

        if (_particle != null)
        {
            _particle.Stop();

            var main = _particle.main;

            main.startLifetime = _weapon.PuddleLifeDuration.Value;
            main.duration = _weapon.PuddleLifeDuration.Value;

            _particle.transform.localScale = new Vector3(_weapon.PuddleRadius.Value, _weapon.PuddleRadius.Value, _weapon.PuddleRadius.Value);

            _particle.Play();
        }
        else if (_isDebug) Debug.Log("Missing particle!");

        //_sounds.PlaySound(SoundTypes.Shoot);
    }

    public void OnUpdate()
    {
        _attackTimer -= Time.deltaTime;
        if (_attackTimer <= 0)
        {
            Attack();
        }

        _releaseTimer -= Time.deltaTime;
        if (_releaseTimer <= 0)
        {
            _weapon.OnPuddleRelease(this);
        }
    }

    private void Attack()
    {
        if (_targetDetector.Targets.Count == 0) return;

        List<GameObject> targets = _targetDetector.Targets;

        if (targets.Count == 0) return;

        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == null) continue;

            DamageableObject target = targets[i].GetComponent<DamageableObject>();

            if (target != null)
            {
                target.TakeDamage((int)_damage.Value);
            }
        }

        _attackTimer = _attackInterval.Value;
    }
}
