using System.Collections;
using UnityEngine;

public class AreaCleaner : PickableObject
{
    [SerializeField] private Radius _explosionRadius;
    [SerializeField] private TargetDetector _targetDetector;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private float _destroyDelay = 1f;

    protected override void OnEnable()
    {
        base.OnEnable();

        _explosionRadius.Initialize();
        _targetDetector.Initialize(_explosionRadius);
    }

    protected override void OnPickUp()
    {
        base.OnPickUp();

        _particle.Play();

        StartCoroutine(WaitDestroy());

        foreach(var target in _targetDetector.Targets)
        {
            DamageableObject obj = target.GetComponent<DamageableObject>();

            if (obj != null) obj.Die();
        }
    }

    private IEnumerator WaitDestroy()
    {
        yield return new WaitForSeconds(_destroyDelay);

        Destroy(gameObject);
    }
}
