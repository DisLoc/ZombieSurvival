using UnityEngine;

[System.Serializable]
public class HordeBreakpoint : Breakpoint
{
    [Header("Enemy settings")]
    [SerializeField] protected Enemy _enemyToSpawnPrefab;
    [SerializeField][Range(1, 200)] protected int _spawnCount;

    public Enemy EnemyToSpawnPrefab => _enemyToSpawnPrefab;
    public int SpawnCount => _spawnCount;

    protected HordeBreakpoint(HordeBreakpoint breakpoint) : base(breakpoint)
    {
        _enemyToSpawnPrefab = breakpoint._enemyToSpawnPrefab;
        _spawnCount = breakpoint._spawnCount;
    }

    public override Breakpoint Clone() => new HordeBreakpoint(this);
}
