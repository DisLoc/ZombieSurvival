using UnityEngine;

[System.Serializable]
public class HordeBreakpoint : Breakpoint
{
    [Header("Enemy settings")]
    [SerializeField] protected Zombie _enemyToSpawnPrefab;
    [SerializeField][Range(1, 200)] protected int _spawnCount;

    public Zombie EnemyToSpawnPrefab => _enemyToSpawnPrefab;
    public int SpawnCount => _spawnCount;
}
