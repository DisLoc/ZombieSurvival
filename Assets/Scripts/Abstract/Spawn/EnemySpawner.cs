using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySpawner : Spawner, IUpdatable, IFixedUpdatable
{
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
