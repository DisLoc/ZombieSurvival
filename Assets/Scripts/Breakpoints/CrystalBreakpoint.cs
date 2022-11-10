using UnityEngine;

[System.Serializable]
public class CrystalBreakpoint : Breakpoint
{
    [Header("Crystal exp settings")]
    [SerializeField] protected CrystalStats _spawningCrystalStats;

    public CrystalStats SpawningCrystalsStats => _spawningCrystalStats;

    protected CrystalBreakpoint(CrystalBreakpoint breakpoint) : base(breakpoint)
    {
        _spawningCrystalStats = breakpoint._spawningCrystalStats;

        foreach(var stats in _spawningCrystalStats.CrystalSpawnParams)
        {
            stats.SpawnChance.Initialize();
        }
    }

    public override Breakpoint Clone() => new CrystalBreakpoint(this);
}
