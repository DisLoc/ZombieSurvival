using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Level/LevelContext", fileName = "New level context")]
public class LevelContext : ScriptableObject
{
    [Header("Level settings")]
    [SerializeField] private LevelBuilder _levelBuilderPrefab;
    [SerializeField] private GroundGrid _levelEnvironment;

    [Header("Breakpoints settings")]
    [SerializeField] private BreakpointList<CrystalBreakpoint> _crystalSpawnBreakpoints;
    [Space(5)]
    [SerializeField] private BreakpointList<EnemyBreakpoint> _enemyBreakpoints;
    [SerializeField] private BreakpointList<HordeBreakpoint> _hordeBreakpoints;
    [SerializeField] private BreakpointList<BossBreakpoint> _bossBreakpoints;

    [Header("Level Upgrades")]
    [SerializeField] private List<Upgrade> _playerUpgrades;
    [SerializeField] private List<Upgrade> _enemiesUpgrades;

    public LevelBuilder LevelBuilder { get; private set; }
    public GroundGrid LevelEnvironment => _levelEnvironment;

    public BreakpointList<CrystalBreakpoint> CrystalSpawnBreakpoints => _crystalSpawnBreakpoints;
    public BreakpointList<HordeBreakpoint> HordeBreakpoints => _hordeBreakpoints;
    public BreakpointList<BossBreakpoint> BossBreakpoints => _bossBreakpoints;

    public List<Upgrade> PlayerUpgrades => _playerUpgrades;
    public List<Upgrade> EnemiesUpgrades => _enemiesUpgrades;

    public void Initialize()
    {
        LevelBuilder = Instantiate(_levelBuilderPrefab);

        LevelBuilder.Construct(_levelEnvironment);
    }
}
