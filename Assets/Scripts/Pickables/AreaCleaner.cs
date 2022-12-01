using System.Collections;
using System.Collections.Generic;
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

        List<GameObject> targets = _targetDetector.Targets;

        if (targets.Count == 0) return;

        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == null) continue;

            DamageableObject target = targets[i].GetComponent<DamageableObject>();

            if (target != null)
            {
                target.Die();
            }
        }

        Destroy(gameObject, _destroyDelay);
    }
}
