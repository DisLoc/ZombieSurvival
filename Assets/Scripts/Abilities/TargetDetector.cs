using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TargetDetector : TriggerDetector, IObjectDisableHandler
{
    [SerializeField] private GameObject _parent;

    private List<GameObject> _targets;

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

    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }
    
    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    public override void Initialize(Radius raduis)
    {
        base.Initialize(raduis);

        _targets = new List<GameObject>();
    }

    public void SetTargetTags(TagList tags)
    {
        _triggerTags = tags;
    }

    public void OnObjectDisable(GameObject obj)
    {
        if (obj == null) return;

        int index = _targets.FindIndex(item => item != null && item.Equals(obj));

        if (index != -1)
        {
            if (_isDebug) Debug.Log(obj.name + " exit");

            _targets[index] = null;
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (_triggerTags.Contains(other.tag))
        {
            if (_isDebug) Debug.Log(other.name + " added");

            _targets.Add(other.gameObject);
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (_triggerTags.Contains(other.tag))
        {
            if (_isDebug) Debug.Log(other.name + " exit");

            _targets.Remove(other.gameObject);
        }
    }

    /// <summary>
    /// Remove all missing objects
    /// </summary>
    public void Cleanup(bool cleanupAll = false)
    {
        if (cleanupAll)
        {
            _targets?.Clear();
            return;
        }

        if (_parent != null && _parent.activeSelf == false)
        {
            _targets?.Clear();
        }
        else
        {
            _targets?.RemoveAll(item => item == null || item.activeSelf == false);
        }
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

    public Vector3 GetRandomTargetPosition()
    {
        Cleanup();

        if (_targets.Count == 0) return -Vector3.one;

        return _targets[Random.Range(0, _targets.Count)].transform.position;
    }

    public Vector3 GetDirectionToRandomTarget()
    {
        Vector3 position = GetRandomTargetPosition();

        if (position == -Vector3.one)
        {
            return transform.TransformDirection(Vector3.forward);
        }

        position.y = transform.position.y;

        return (position - transform.position).normalized;
    }
}
