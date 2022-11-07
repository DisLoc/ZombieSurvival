using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public sealed class ShellingExplosion : MonoBehaviour, IPoolable
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private ParticleSystem _impact1;
    [SerializeField] private ParticleSystem _impact2;
    [SerializeField] private ParticleSystem _impact3;
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private TagList _targetTags;

    private Shelling _weapon;

    private float _releaseTimer;

    private Damage _damage;

    public void ResetObject()
    {
        _weapon = null;
        _damage = null;
        _releaseTimer = 0;
    }

    public void Initialize(ProjectileAbilityStats stats, Shelling weapon)
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
        }
        
        if (_impact1 != null)
        {
            _impact1.Stop();

            _impact1.transform.localScale = new Vector3(stats.ProjectileSize.Value, stats.ProjectileSize.Value, stats.ProjectileSize.Value);

            _impact1.Play();
        }
        else
        {
            if (_isDebug) Debug.Log("Missing impact1!");
        }

        if (_impact2 != null)
        {
            _impact2.Stop();

            _impact2.transform.localScale = new Vector3(stats.ProjectileSize.Value, stats.ProjectileSize.Value, stats.ProjectileSize.Value);

            _impact2.Play();
        }
        else
        {
            if (_isDebug) Debug.Log("Missing impact1!");
        }

        if (_impact3 != null)
        {
            _impact3.Stop();

            _impact3.transform.localScale = new Vector3(stats.ProjectileSize.Value, stats.ProjectileSize.Value, stats.ProjectileSize.Value);

            _impact3.Play();
        }
        else
        {
            if (_isDebug) Debug.Log("Missing impact1!");
        }

        transform.localScale = new Vector3(stats.ProjectileSize.Value, stats.ProjectileSize.Value, stats.ProjectileSize.Value);
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

        if (obj != null && _targetTags.Contains(other.tag))
        {
            obj.TakeDamage((int)_damage.Value);

            if (_isDebug) Debug.Log("Find target: " + other.name);
        }
    }
}
