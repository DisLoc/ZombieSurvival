using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public sealed class ShellingExplosion : MonoBehaviour, IPoolable
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private Tags _targetTag;

    private PlayerShelling _weapon;

    private float _releaseTimer;

    private Damage _damage;

    public void ResetObject()
    {
        _weapon = null;
        _damage = null;
        _releaseTimer = 0;
    }

    public void Initialize(ProjectileAbilityStats stats, PlayerShelling weapon)
    {
        _weapon = weapon;
        _sphereCollider.radius = _weapon.ExplosionRadius.Value;

        _releaseTimer = _weapon.ExplosionLifeDuration.Value;
        _damage = stats.Damage;

        if (_particle != null)
        {
            _particle.Stop();

            var main = _particle.main;

            main.startLifetime = _weapon.ExplosionLifeDuration.Value;
            main.duration = _weapon.ExplosionLifeDuration.Value;

            _particle.transform.localScale = new Vector3(stats.ProjectileSize.Value, stats.ProjectileSize.Value, stats.ProjectileSize.Value);

            _particle.Play();
        }
        else
        {
            if (_isDebug) Debug.Log("Missing particle!");

            transform.localScale = new Vector3(stats.ProjectileSize.Value, stats.ProjectileSize.Value, stats.ProjectileSize.Value);
        }
    }

    public void OnUpdate()
    {
        _releaseTimer -= Time.deltaTime;

        if (_releaseTimer <= 0)
        {
            if (_isDebug) Debug.Log("Releasing: " + name);

            _weapon.OnExplosionRelease(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        DamageableObject obj = other.GetComponent<DamageableObject>();

        if (obj != null && other.CompareTag(_targetTag.ToString()))
        {
            obj.TakeDamage((int)_damage.Value);

            if (_isDebug) Debug.Log("Find target: " + other.name);
        }
    }
}
