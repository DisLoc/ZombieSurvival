using UnityEngine;
using Zenject;

public abstract class EnemySpawner : Spawner, IUpdatable, IFixedUpdatable
{
    [Tooltip("Means distance between player and spawned objects")]
    [SerializeField] protected float _maxDistanceForRespawn;

    [Inject] protected Player _player;
    [Inject] protected LevelContext _levelContext;

    protected override void OnEnable()
    {
        base.OnEnable();;    
    }

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
