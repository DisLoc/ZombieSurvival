using UnityEngine;

public class LegendaryZombieBreakpoint : Breakpoint
{
    [Header("Enemy settings")]
    [SerializeField] protected Enemy _enemyToSpawnPrefab;
    [SerializeField] [Range(1, 200)] protected int _spawnCount;

    public Enemy EnemyToSpawnPrefab => _enemyToSpawnPrefab;
    public int SpawnCount => _spawnCount;

    protected LegendaryZombieBreakpoint(LegendaryZombieBreakpoint breakpoint) : base(breakpoint)
    {
        _enemyToSpawnPrefab = breakpoint._enemyToSpawnPrefab;
        _spawnCount = breakpoint._spawnCount;
    }

    public override Breakpoint Clone() => new LegendaryZombieBreakpoint(this);
}
