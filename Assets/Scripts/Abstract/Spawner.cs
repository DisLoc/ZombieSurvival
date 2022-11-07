using UnityEngine;

public abstract class Spawner : MonoBehaviour, ILevelProgressUpdateHandler, IUpdatable, IFixedUpdatable
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    [Header("Settings")]
    [SerializeField][Range(0, 50)] protected float _spawnDeltaDistance;

    protected abstract void Spawn(Vector3 position);

    public virtual void OnLevelProgressUpdate(int progress) { }

    public virtual void OnUpdate() { }

    public virtual void OnFixedUpdate() { }

    protected virtual void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    protected virtual void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }
}
