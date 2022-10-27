using System.Collections.Generic;
using UnityEngine;

public class CrystalBreakpoint : Breakpoint
{
    [Header("Crystal exp settings")]
    [SerializeField] protected List<ObjectChanceSpawn<CrystalParam>> _spawningCrystalParams;

    public List<ObjectChanceSpawn<CrystalParam>> SpawningCrystalsParams => _spawningCrystalParams;
}
