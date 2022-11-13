using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class FatZombieExplosion : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private ParticleSystem _impact1;
    [SerializeField] private ParticleSystem _impact2;
    [SerializeField] private SphereCollider _sphereCollider;

    private float _releaseTimer;

    private Damage _damage;
    private TagList _targetTags;

    public void Initialize(FatZombieExplosionStats stats, TagList collisionTags)
    {
        _damage = stats.ExplosionDamage;
        _releaseTimer = stats.ExplosionDuration.Value;
        _targetTags = collisionTags;

        if (_particle != null)
        {
            _particle.Stop();

            var main = _particle.main;

            main.startLifetime = stats.ExplosionDuration.Value;
            main.duration = stats.ExplosionDuration.Value;

            _particle.transform.localScale = new Vector3(stats.ExplosionRadius.Value, stats.ExplosionRadius.Value, stats.ExplosionRadius.Value);

            _particle.Play();
        }
        else
        {
            if (_isDebug) Debug.Log("Missing particle!");
        }

        if (_impact1 != null)
        {
            _impact1.Stop();

            _impact1.transform.localScale = new Vector3(stats.ExplosionRadius.Value, stats.ExplosionRadius.Value, stats.ExplosionRadius.Value);

            _impact1.Play();
        }
        else
        {
            if (_isDebug) Debug.Log("Missing impact1!");
        }

        if (_impact2 != null)
        {
            _impact2.Stop();

            _impact2.transform.localScale = new Vector3(stats.ExplosionRadius.Value, stats.ExplosionRadius.Value, stats.ExplosionRadius.Value);

            _impact2.Play();
        }
        else
        {
            if (_isDebug) Debug.Log("Missing impact2!");
        }

        transform.localScale = new Vector3(stats.ExplosionRadius.Value, stats.ExplosionRadius.Value, stats.ExplosionRadius.Value);
    }

    public void Update()
    {
        _releaseTimer -= Time.deltaTime;

        if (_releaseTimer <= 0)
        {
            if (_isDebug) Debug.Log("Destroying: " + name);

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        DamageableObject obj = other.GetComponent<DamageableObject>();

        if (obj != null && _targetTags.Contains(other.tag))
        {
            if (_isDebug) Debug.Log("Find target: " + other.name);

            obj.TakeDamage((int)_damage.Value);
        }
    }
}