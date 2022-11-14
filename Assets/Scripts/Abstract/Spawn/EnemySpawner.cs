using UnityEngine;

public abstract class EnemySpawner : Spawner, IUpdatable, IFixedUpdatable
{
    [Tooltip("Means distance between player and spawned objects")]
    [SerializeField] protected float _maxDistanceForRespawn;

    public virtual void OnUpdate() { }

    public virtual void OnFixedUpdate() { }



    /// <summary>
    /// Add upgrade to enemies (spawned and enemies in pool)
    /// </summary>
    protected abstract void GetUpgrade();

    /// <summary>
    /// Dispel upgrade from enemies (spawned and enemies in pool)
    /// </summary>
    protected abstract void DispelUpgrades();
}
