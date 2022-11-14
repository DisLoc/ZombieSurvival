using UnityEngine;

[System.Serializable]
public class EliteZombieBreakpoint : Breakpoint
{
    [Header("Enemy settings")]
    [SerializeField] protected Enemy _enemyToSpawnPrefab;
    [SerializeField] private int _abilitiesRewardCount;

    public Enemy EnemyToSpawnPrefab => _enemyToSpawnPrefab;
    public int AbilitiesRewardCount => _abilitiesRewardCount;

    protected EliteZombieBreakpoint(EliteZombieBreakpoint breakpoint) : base(breakpoint)
    {
        _enemyToSpawnPrefab = breakpoint._enemyToSpawnPrefab;
        _abilitiesRewardCount = breakpoint._abilitiesRewardCount;
    }

    public override Breakpoint Clone() => new EliteZombieBreakpoint(this);
}
