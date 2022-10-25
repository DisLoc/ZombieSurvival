using UnityEngine;

public sealed class ObjectCatcher : TriggerDetector
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_triggerTag.ToString()))
        {
            PickableObject obj = other.GetComponent<PickableObject>();

            if (obj != null)
            {
                obj.PickUp();
            }
            else if (_isDebug) Debug.Log("Missing pickable object!");
        }
    }
}
