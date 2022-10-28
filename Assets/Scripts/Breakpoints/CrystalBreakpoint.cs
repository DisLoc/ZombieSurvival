using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CrystalBreakpoint : Breakpoint
{
    [Header("Crystal exp settings")]
    [SerializeField] protected CrystalStats _spawningCrystalStats;

    public CrystalStats SpawningCrystalsStats => _spawningCrystalStats;
}
