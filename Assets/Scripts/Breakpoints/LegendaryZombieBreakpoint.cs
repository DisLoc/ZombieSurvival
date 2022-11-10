using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendaryZombieBreakpoint : Breakpoint
{
    [Header("Enemy settings")]
    [SerializeField] protected Enemy _enemyToSpawnPrefab;
    [SerializeField] [Range(1, 200)] protected int _spawnCount;

    public Enemy EnemyToSpawnPrefab => _enemyToSpawnPrefab;
    public int SpawnCount => _spawnCount;
}
