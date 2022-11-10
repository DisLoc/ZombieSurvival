using UnityEngine;

public abstract class Spawner : MonoBehaviour, ILevelProgressUpdateHandler
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    [Header("Settings")]
    [SerializeField][Range(0, 50)] protected float _spawnDeltaDistance;

    protected abstract void Spawn(Vector3 position);

    public virtual void OnLevelProgressUpdate(int progress) { }

    protected virtual void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    protected virtual void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }
}
