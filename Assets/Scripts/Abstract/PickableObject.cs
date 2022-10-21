using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class PickableObject : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;
    [SerializeField] protected Color _gizmosColor;

    [Header("Settings")]
    [SerializeField] protected SphereCollider _pickUpCollider;
    [SerializeField] protected float _pickUpRange;

    private void OnEnable()
    {
        _pickUpCollider.isTrigger = true;
        _pickUpCollider.radius = _pickUpRange;
        gameObject.tag = Tags.PickableObject.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Return self</returns>
    public virtual PickableObject PickUp()
    {
        if (_isDebug) Debug.Log("Pick up " + name);

        return this;
    }

    private void OnDrawGizmosSelected()
    {
        if (_isDebug)
        {
            Gizmos.color = _gizmosColor;
            Gizmos.DrawWireSphere(transform.position, _pickUpRange * transform.localScale.y);
        }
    }
}
