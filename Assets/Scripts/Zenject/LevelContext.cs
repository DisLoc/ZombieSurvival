using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Level/LevelContext", fileName = "New level context")]
public sealed class LevelContext : ScriptableObject
{
    [Header("Level settings")]
    [SerializeField] private string _levelName;
    [SerializeField] private int _levelNumber;
    [SerializeField] private Sprite _levelIcon;

    [Space(5)]
    [Tooltip("Level lenght in minutes")]
    [SerializeField][Range(1, 60)] private int _levelLenght;
    [SerializeField] private Reward _levelReward;

    [Space(5)]
    [SerializeField] private GroundGrid _levelEnvironment;

    [Header("Breakpoints settings")]
    [SerializeField] private BreakpointList<CrystalBreakpoint> _crystalSpawnBreakpoints;

    [Space(5)]
    [SerializeField] private BreakpointList<EnemyBreakpoint> _enemyBreakpoints;
    [SerializeField] private BreakpointList<HordeBreakpoint> _hordeBreakpoints;
    //[SerializeField] private BreakpointList<HerdBreakpoint> _herdBreakpoints;
    [SerializeField] private BreakpointList<EliteZombieBreakpoint> _eliteZombieBreakpoints;
    [SerializeField] private BreakpointList<BossBreakpoint> _bossBreakpoints;

    [Space(5)]
    [SerializeField] private BreakpointList<UpgradeBreakpoint> _enemyUpgradeBreakpoints;

    [Header("Level Upgrades")]
    [SerializeField] private List<Upgrade> _playerUpgrades;
    [SerializeField] private List<Upgrade> _enemiesUpgrades;

    public string LevelName => _levelName;
    public int LevelNumber => _levelNumber;
    public Sprite LevelIcon => _levelIcon;
    /// <summary>
    /// Level lenght in seconds
    /// </summary>
    public int LevelLenght => _levelLenght * 60;
    public Reward LevelReward => _levelReward;

    public LevelBuilder LevelBuilder { get; private set; }
    public GroundGrid LevelEnvironment => _levelEnvironment;

    public BreakpointList<CrystalBreakpoint> CrystalSpawnBreakpoints => _crystalSpawnBreakpoints;

    public BreakpointList<EnemyBreakpoint> EnemyBreakpoints => _enemyBreakpoints;
    public BreakpointList<HordeBreakpoint> HordeBreakpoints => _hordeBreakpoints;
    //public BreakpointList<HerdBreakpoint> HerdBreakpoints => _herdBreakpoints;
    public BreakpointList<EliteZombieBreakpoint> EliteZombieBreakpoints => _eliteZombieBreakpoints;
    public BreakpointList<BossBreakpoint> BossBreakpoints => _bossBreakpoints;

    public BreakpointList<UpgradeBreakpoint> EnemyUpgradeBreakpoints => _enemyUpgradeBreakpoints;

    public List<Upgrade> PlayerUpgrades => _playerUpgrades;
    public List<Upgrade> EnemiesUpgrades => _enemiesUpgrades;

    public void Initialize(LevelBuilder builderPrefab)
    {
        LevelBuilder = Instantiate(builderPrefab);

        LevelBuilder.Construct(_levelEnvironment);
    }
}
