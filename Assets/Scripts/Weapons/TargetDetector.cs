using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : TriggerDetector
{
    private List<GameObject> _targets;

    public List<GameObject> Targets => _targets;

    public override void Initialize()
    {
        base.Initialize();

        _targets = new List<GameObject>();
    }

    private void OnEnable()
    {
        Initialize();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_triggerTag.ToString()))
        {
            _targets.Add(other.gameObject);
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_triggerTag.ToString()))
        {
            _targets.Remove(other.gameObject);
        }
    }
}
