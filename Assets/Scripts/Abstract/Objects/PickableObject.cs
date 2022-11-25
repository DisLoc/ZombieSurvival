using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class PickableObject : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;
    [SerializeField] protected Color _gizmosColor;

    [Header("Pick up settings")]
    [SerializeField] protected TagList _pickUpTags;
    [SerializeField] protected SphereCollider _pickUpCollider;
    [SerializeField] protected float _pickUpRange;

    [SerializeField][Range(0.01f, 20f)] protected float _pickUpSpeed = 13;
    [Tooltip("Speed increases every frame while moving")]
    [SerializeField][Range(0f, 0.1f)] protected float _speedMultiplier = 0.01f;

    [Header("Sounds settings")]
    [SerializeField] protected SoundList _sounds;

    protected float _speed;
    protected Transform _target;

    protected virtual void OnEnable()
    {
        _pickUpCollider.isTrigger = true;
        _pickUpCollider.radius = _pickUpRange;
    }

    public virtual void PickUp(Transform target)
    {
        if (_isDebug) Debug.Log("Pick up " + name);

        _target = target;
        _speed = _pickUpSpeed;

        StartCoroutine(MoveToTarget());
    }

    private IEnumerator MoveToTarget()
    {
        if (_target == null)
        {
            if (_isDebug) Debug.Log("Missing target!");

            yield return null;
        }

        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.fixedDeltaTime);

        _speed += _speed * _speedMultiplier;

        yield return new WaitForFixedUpdate();

        StartCoroutine(MoveToTarget());
    }

    protected virtual void OnPickUp()
    {
        _sounds.PlaySound(SoundTypes.PickUp);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (_pickUpTags.Contains(other.tag))
        {
            OnPickUp();
        }
    }

    protected virtual void OnDrawGizmosSelected()
    {
        if (_isDebug)
        {
            Gizmos.color = _gizmosColor;
            Gizmos.DrawWireSphere(transform.position, _pickUpRange * transform.localScale.y);
        }
    }
}
