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

    protected EnemyBreakpoint(EnemyBreakpoint breakpoint) : base(breakpoint)
    {
        _spawningEnemies = breakpoint._spawningEnemies;
        _maxUnitsOnScene = breakpoint._maxUnitsOnScene;

        foreach(var spawning in _spawningEnemies)
        {
            spawning.SpawnChance.Initialize();
        }
    }

    public override Breakpoint Clone() => new EnemyBreakpoint(this);
}
