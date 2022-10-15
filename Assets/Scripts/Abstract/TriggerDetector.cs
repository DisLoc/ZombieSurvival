using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class TriggerDetector : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    [Header("Settings")]
    [SerializeField] protected SphereCollider _collider;
    [SerializeField] protected Tags _triggerTag;

    public virtual void Initialize()
    {
        _collider.isTrigger = true;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (_isDebug && other.CompareTag(_triggerTag.ToString()))
        {
            Debug.Log(name + " detected " + other.name);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (_isDebug && other.CompareTag(_triggerTag.ToString()))
        {
            Debug.Log(other.name + " exit");
        }
    }
}
