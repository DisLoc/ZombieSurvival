using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : TriggerDetector
{
    private List<GameObject> _targets;
    private const float MAX_DELTA_POS = 1f;

    /// <summary>
    /// Detected objects
    /// </summary>
    public List<GameObject> Targets
    {
        get
        {
            Cleanup();

            return _targets;
        }
    }

    public override void Initialize(Radius raduis)
    {
        base.Initialize(raduis);

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

    /// <summary>
    /// Remove all missing objects
    /// </summary>
    protected void Cleanup()
    {
        _targets.RemoveAll(item => item == null || item.activeSelf == false);
    }


    public Vector3 GetNearestTargetPosition()
    {
        Cleanup();

        if (_targets.Count == 0) return -Vector3.one;

        float minMagnitude = (_targets[0].transform.position - transform.position).magnitude;
        int minIndex = 0;
        
        for(int i = 1; i < _targets.Count; i++)
        {
            float magnitude = (_targets[i].transform.position - transform.position).magnitude;
            if (magnitude < minMagnitude)
            {
                minIndex = i;
                minMagnitude = magnitude;
            }
        }

        return _targets[minIndex].transform.position;
    }

    public Vector3 GetDirectionToNearestTarget()
    {
        Vector3 position = GetNearestTargetPosition();

        if (position == -Vector3.one)
        {
            return transform.TransformDirection(Vector3.forward);
        }

        position.y = transform.position.y;

        return (position - transform.position).normalized;
    }

    public Vector3 GetFarthestTargetPosition()
    {
        Cleanup();

        if (_targets.Count == 0) return -Vector3.one;

        float maxMagnitude = (_targets[0].transform.position - transform.position).magnitude;
        int maxIndex = 0;

        for (int i = 1; i < _targets.Count; i++)
        {
            float magnitude = (_targets[i].transform.position - transform.position).magnitude;
            if (magnitude > maxMagnitude)
            {
                maxIndex = i;
                maxMagnitude = magnitude;
            }
        }

        return _targets[maxIndex].transform.position;
    }

    public Vector3 GetDirectionToFarthestTarget()
    {
        Vector3 position = GetFarthestTargetPosition();

        if (position == -Vector3.one)
        {
            return transform.TransformDirection(Vector3.forward);
        }

        position.y = transform.position.y;

        return (position - transform.position).normalized;
    }
}
