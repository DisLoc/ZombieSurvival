using UnityEngine;

[System.Serializable]
public class EliteZombieBreakpoint : Breakpoint
{
    [Header("Enemy settings")]
    [SerializeField] protected Enemy _enemyToSpawnPrefab;
    [SerializeField] private int _maxAbilitiesRewardCount;

    public Enemy EnemyToSpawnPrefab => _enemyToSpawnPrefab;
    public int MaxAbilitiesRewardCount => _maxAbilitiesRewardCount;

    protected EliteZombieBreakpoint(EliteZombieBreakpoint breakpoint) : base(breakpoint)
    {
        _enemyToSpawnPrefab = breakpoint._enemyToSpawnPrefab;
        _maxAbilitiesRewardCount = breakpoint._maxAbilitiesRewardCount;
    }

    public override Breakpoint Clone() => new EliteZombieBreakpoint(this);
}
