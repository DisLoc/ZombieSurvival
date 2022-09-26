using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public sealed class ObjectCatcher : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private Tags _catchTag;
    [SerializeField] private SphereCollider _pickUpCollider;

    public void Initialize(float pickUpRange)
    {
        _pickUpCollider.radius = pickUpRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == _catchTag.ToString())
        {
            if (_isDebug) Debug.Log("Pick up " + other.name);
            
            other.GetComponent<PickableObject>().PickUp();
        }
    }
}
