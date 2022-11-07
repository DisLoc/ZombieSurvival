using UnityEngine;

public sealed class ObjectCatcher : TriggerDetector
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (_triggerTags.Contains(other.tag))
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
