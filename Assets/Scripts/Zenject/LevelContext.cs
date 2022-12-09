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
    [SerializeField] private LevelRewardBreakpoints _levelRewards;

    [Space(5)]
    [SerializeField] private GroundGrid _levelEnvironment;

    [Space(5)]
    [SerializeField] private CrystalStats _startCrystalsStats;
    [SerializeField] private int _startCrystalsCount = 8;

    [Header("Breakpoints settings")]
    [SerializeField] private BreakpointList<CrystalBreakpoint> _crystalSpawnBreakpoints;

    [Space(5)]
    [SerializeField] private BreakpointList<EnemyBreakpoint> _enemyBreakpoints;
    [SerializeField] private BreakpointList<HordeBreakpoint> _hordeBreakpoints;
    [SerializeField] private BreakpointList<HerdBreakpoint> _herdBreakpoints;
    [SerializeField] private BreakpointList<EliteZombieBreakpoint> _eliteZombieBreakpoints;
    [SerializeField] private BreakpointList<BossBreakpoint> _bossBreakpoints;

    [Space(5)]
    [SerializeField] private BreakpointList<UpgradeBreakpoint> _enemyUpgradeBreakpoints;

    [Header("Level Upgrades")]
    [SerializeField] private List<Upgrade> _playerUpgrades;
    [SerializeField] private List<Upgrade> _enemiesUpgrades;

    #region Fields
    public string LevelName => _levelName;
    public int LevelNumber => _levelNumber;
    public Sprite LevelIcon => _levelIcon;
    /// <summary>
    /// Level lenght in seconds
    /// </summary>
    public int LevelLenght => _levelLenght * 60;
    public int LevelLenghtInMinutes => _levelLenght;
    public LevelRewardBreakpoints LevelRewards => _levelRewards;

    public LevelBuilder LevelBuilder { get; private set; }

    public CrystalStats StartCrystalStats => _startCrystalsStats;
    public int StartCrystalsCount => _startCrystalsCount;

    public BreakpointList<CrystalBreakpoint> CrystalSpawnBreakpoints => _crystalSpawnBreakpoints;

    public BreakpointList<EnemyBreakpoint> EnemyBreakpoints => _enemyBreakpoints;
    public BreakpointList<HordeBreakpoint> HordeBreakpoints => _hordeBreakpoints;
    public BreakpointList<HerdBreakpoint> HerdBreakpoints => _herdBreakpoints;
    public BreakpointList<EliteZombieBreakpoint> EliteZombieBreakpoints => _eliteZombieBreakpoints;
    public BreakpointList<BossBreakpoint> BossBreakpoints => _bossBreakpoints;

    public BreakpointList<UpgradeBreakpoint> EnemyUpgradeBreakpoints => _enemyUpgradeBreakpoints;

    #region All breakpoints
    public BreakpointList<Breakpoint> AllBreakpoints
    {
        get
        {
            BreakpointList<Breakpoint> breakpoints = new BreakpointList<Breakpoint>();

            for (int i = 0; i < GetMaxBreakpointsCount(); i++)
            {
                if (i < _crystalSpawnBreakpoints.Breakpoints.Count)
                {
                    breakpoints.Add(_crystalSpawnBreakpoints.Breakpoints[i]);
                }

                if (i < _enemyBreakpoints.Breakpoints.Count)
                {
                    breakpoints.Add(_enemyBreakpoints.Breakpoints[i]);
                }

                if (i < _hordeBreakpoints.Breakpoints.Count)
                {
                    breakpoints.Add(_hordeBreakpoints.Breakpoints[i]);
                }

                if (i < _herdBreakpoints.Breakpoints.Count)
                {
                    breakpoints.Add(_herdBreakpoints.Breakpoints[i]);
                }

                if (i < _eliteZombieBreakpoints.Breakpoints.Count)
                {
                    breakpoints.Add(_eliteZombieBreakpoints.Breakpoints[i]);
                }

                if (i < _bossBreakpoints.Breakpoints.Count)
                {
                    breakpoints.Add(_bossBreakpoints.Breakpoints[i]);
                }

                if (i < _enemyUpgradeBreakpoints.Breakpoints.Count)
                {
                    breakpoints.Add(_enemyUpgradeBreakpoints.Breakpoints[i]);
                }
            }

            return breakpoints;
        }
    }

    private int GetMaxBreakpointsCount()
    {
        int max = _crystalSpawnBreakpoints.Breakpoints.Count;

        if (max < _enemyBreakpoints.Breakpoints.Count)
        {
            max = _enemyBreakpoints.Breakpoints.Count;
        }

        if (max < _hordeBreakpoints.Breakpoints.Count)
        {
            max = _hordeBreakpoints.Breakpoints.Count;
        }

        if (max < _herdBreakpoints.Breakpoints.Count)
        {
            max = _herdBreakpoints.Breakpoints.Count;
        }

        if (max < _eliteZombieBreakpoints.Breakpoints.Count)
        {
            max = _eliteZombieBreakpoints.Breakpoints.Count;
        }

        if (max < _bossBreakpoints.Breakpoints.Count)
        {
            max = _bossBreakpoints.Breakpoints.Count;
        }

        if (max < _enemyUpgradeBreakpoints.Breakpoints.Count)
        {
            max = _enemyUpgradeBreakpoints.Breakpoints.Count;
        }

        return max;
    }
    #endregion

    public List<Upgrade> PlayerUpgrades => _playerUpgrades;
    public List<Upgrade> EnemiesUpgrades => _enemiesUpgrades;
    #endregion

    [HideInInspector] public bool wasPassed;
    [HideInInspector] public int maxSurvivalTime;

    public void Initialize(LevelBuilder builderPrefab)
    {
        LevelBuilder = Instantiate(builderPrefab);
        LevelBuilder.Construct(_levelEnvironment);
    }

    #region DEBUG
    [ContextMenu("Reset level")]
    private void ResetLevel()
    {
        wasPassed = false;
        maxSurvivalTime = -1;

        foreach (var breakpoint in _levelRewards.Breakpoints)
        {
            breakpoint.SetReached(false);
            breakpoint.wasClaimed = false;
        }
    }
    
    [ContextMenu("Pass level")]
    private void PassLevel()
    {
        wasPassed = true;

        foreach (var breakpoint in _levelRewards.Breakpoints)
        {
            breakpoint.SetReached(true);
            breakpoint.wasClaimed = false;
        }

        maxSurvivalTime = _levelRewards.Breakpoints[_levelRewards.Breakpoints.Count - 1].RequiredTime;
    }

    [ContextMenu("Pass next breakpoint")]
    private void PassNextBreakpoint()
    {
        Breakpoint breakpoint = _levelRewards.CheckReaching(100);

        if (breakpoint != null)
        {
            maxSurvivalTime = (breakpoint as LevelBreakpoint).RequiredTime;
        }
    }
    #endregion
}
