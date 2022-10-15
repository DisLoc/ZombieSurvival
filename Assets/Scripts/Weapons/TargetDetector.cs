using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : TriggerDetector
{
    private List<GameObject> _targets;

    public List<GameObject> Targets
    {
        get
        {
            Cleanup();

            return _targets;
        }
    }

    public override void Initialize()
    {
        base.Initialize();

        _targets = new List<GameObject>();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_triggerTag.ToString()))
        {
            if (_isDebug) Debug.Log(other.name + " enter");

            _targets.Add(other.gameObject);
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_triggerTag.ToString()))
        {
            if (_isDebug) Debug.Log(other.name + " exit");

            _targets.Remove(other.gameObject);
        }
    }

    protected void Cleanup()
    {
        _targets.RemoveAll(item => item == null || item.activeSelf == false);
    }
}
