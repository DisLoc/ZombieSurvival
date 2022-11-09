using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Level/LevelContext", fileName = "New level context")]
public class LevelContext : ScriptableObject
{
    [Header("Level settings")]
    [SerializeField] private string _levelName;
    [SerializeField] private int _levelNumber;
    [SerializeField] private Sprite _levelSprite;

    [SerializeField] private LevelBuilder _levelBuilderPrefab;
    [SerializeField] private GroundGrid _levelEnvironment;
    [SerializeField] private LevelReward _levelReward;

    [Header("Breakpoints settings")]
    [SerializeField] private BreakpointList<CrystalBreakpoint> _crystalSpawnBreakpoints;

    [Space(5)]
    [SerializeField] private BreakpointList<EnemyBreakpoint> _enemyBreakpoints;
    [SerializeField] private BreakpointList<HordeBreakpoint> _hordeBreakpoints;
    [SerializeField] private BreakpointList<BossBreakpoint> _bossBreakpoints;

    [Space(5)]
    [SerializeField] private BreakpointList<UpgradeBreakpoint> _enemyUpgradeBreakpoints;

    [Header("Level Upgrades")]
    [SerializeField] private List<Upgrade> _playerUpgrades;
    [SerializeField] private List<Upgrade> _enemiesUpgrades;

    public string LevelName => _levelName;
    public int LevelNumber => _levelNumber;
    public LevelReward LevelReward => _levelReward;

    public LevelBuilder LevelBuilder { get; private set; }
    public GroundGrid LevelEnvironment => _levelEnvironment;

    public BreakpointList<CrystalBreakpoint> CrystalSpawnBreakpoints => _crystalSpawnBreakpoints;
    public BreakpointList<EnemyBreakpoint> EnemyBreakpoints => _enemyBreakpoints;
    public BreakpointList<HordeBreakpoint> HordeBreakpoints => _hordeBreakpoints;
    public BreakpointList<BossBreakpoint> BossBreakpoints => _bossBreakpoints;
    public BreakpointList<UpgradeBreakpoint> EnemyUpgradeBreakpoints => _enemyUpgradeBreakpoints;

    public List<Upgrade> PlayerUpgrades => _playerUpgrades;
    public List<Upgrade> EnemiesUpgrades => _enemiesUpgrades;

    public void Initialize()
    {
        LevelBuilder = Instantiate(_levelBuilderPrefab);

        LevelBuilder.Construct(_levelEnvironment);

        ResetBreakpoints();
    }

    private void ResetBreakpoints()
    {
        for (int i = 0; i < _crystalSpawnBreakpoints.Breakpoints.Count; i++)
        {
            _crystalSpawnBreakpoints.Breakpoints[i].SetReached(false);
        }

        for (int i = 0; i < _enemyBreakpoints.Breakpoints.Count; i++)
        {
            _enemyBreakpoints.Breakpoints[i].SetReached(false);
        }

        for (int i = 0; i < _hordeBreakpoints.Breakpoints.Count; i++)
        {
            _hordeBreakpoints.Breakpoints[i].SetReached(false);
        }

        for (int i = 0; i < _bossBreakpoints.Breakpoints.Count; i++)
        {
            _enemyUpgradeBreakpoints.Breakpoints[i].SetReached(false);
        }

        for (int i = 0; i < _bossBreakpoints.Breakpoints.Count; i++)
        {
            _enemyUpgradeBreakpoints.Breakpoints[i].SetReached(false);
        }
    }
}
