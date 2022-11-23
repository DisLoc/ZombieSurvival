using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PickableMagnet : PickableObject
{
    [Header("Magnet settings")]
    [SerializeField] private Radius _magnetRadius;
    [SerializeField] private TargetDetector _targetDetector;

    [ContextMenu("Initialize")]
    public void Initialize()
    {
        _magnetRadius.Initialize();
        _targetDetector.Initialize(_magnetRadius);
    }

    protected override void OnPickUp()
    {
        foreach(var target in _targetDetector.Targets) 
        {
            PickableObject obj = target.GetComponent<PickableObject>();

            if (obj != null)
            {
                obj.PickUp(_target);
            }
        }

        Destroy(gameObject);
    }
}
