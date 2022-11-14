using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickablesChest : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private Collider _collider;
    [SerializeField] private TagList _triggerTags;

    private ChestSpawner _chestSpawner;

    public void Initialize(ChestSpawner chestSpawner)
    {
        _chestSpawner = chestSpawner;
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isDebug) Debug.Log(other.tag);

        if (_chestSpawner != null)
        {
            if (_triggerTags.Contains(other.tag))
            {
                _chestSpawner.OnChestDestoyed(this);
            }
        }
        else if (_isDebug) Debug.Log("Missing ChestSpawner!");
    }
}
