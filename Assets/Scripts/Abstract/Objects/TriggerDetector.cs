using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class TriggerDetector : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    [Header("Settings")]
    [SerializeField] protected SphereCollider _collider;
    [SerializeField] protected TagList _triggerTags;

    protected Radius _radius;

    public TagList TriggerTags => _triggerTags;

    public virtual void Initialize(Radius raduis)
    {
        _collider.isTrigger = true;

        _radius = raduis;
        UpdateRadius();
    }

    public virtual void UpdateRadius()
    {
        _collider.radius = _radius.Value;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (_isDebug && _triggerTags.Contains(other.tag))
        {
            Debug.Log(name + " detected " + other.name);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (_isDebug && _triggerTags.Contains(other.tag))
        {
            Debug.Log(other.name + " exit");
        }
    }
}
