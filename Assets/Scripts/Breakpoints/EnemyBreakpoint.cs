using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyBreakpoint : Breakpoint
{
    [Header("Enemy settings")]
    [SerializeField] protected List<ObjectChanceSpawn<Enemy>> _spawningEnemies;
    [SerializeField][Range(1, 200)] protected int _maxUnitsOnScene;

    public List<ObjectChanceSpawn<Enemy>> SpawningEnemies => _spawningEnemies;
    public int MaxUnitsOnScene => _maxUnitsOnScene;
}
