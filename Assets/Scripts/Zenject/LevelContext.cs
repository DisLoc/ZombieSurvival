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
    [SerializeField] private BreakpointList<LevelBreakpoint> _levelRewards;

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
    //[SerializeField] private BreakpointList<HerdBreakpoint> _herdBreakpoints;
    [SerializeField] private BreakpointList<EliteZombieBreakpoint> _eliteZombieBreakpoints;
    [SerializeField] private BreakpointList<BossBreakpoint> _bossBreakpoints;

    [Space(5)]
    [SerializeField] private BreakpointList<UpgradeBreakpoint> _enemyUpgradeBreakpoints;

    [Header("Level Upgrades")]
    [SerializeField] private List<Upgrade> _playerUpgrades;
    [SerializeField] private List<Upgrade> _enemiesUpgrades;

    private List<Upgrade> _equipmentUpgrades;

    [HideInInspector] public bool wasPassed;

    #region Fields
    public string LevelName => _levelName;
    public int LevelNumber => _levelNumber;
    public Sprite LevelIcon => _levelIcon;
    /// <summary>
    /// Level lenght in seconds
    /// </summary>
    public int LevelLenght => _levelLenght * 60;
    public BreakpointList<LevelBreakpoint> LevelRewards => _levelRewards;

    public LevelBuilder LevelBuilder { get; private set; }

    public CrystalStats StartCrystalStats => _startCrystalsStats;
    public int StartCrystalsCount => _startCrystalsCount;

    public BreakpointList<CrystalBreakpoint> CrystalSpawnBreakpoints => _crystalSpawnBreakpoints;

    public BreakpointList<EnemyBreakpoint> EnemyBreakpoints => _enemyBreakpoints;
    public BreakpointList<HordeBreakpoint> HordeBreakpoints => _hordeBreakpoints;
    //public BreakpointList<HerdBreakpoint> HerdBreakpoints => _herdBreakpoints;
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

    public Weapon PlayerBaseWeapon { get; private set; }
    public List<Upgrade> PlayerUpgrades
    {
        get
        {
            List<Upgrade> upgrades = new List<Upgrade>();

            upgrades.AddRange(_playerUpgrades);
            upgrades.AddRange(_equipmentUpgrades);

            return upgrades;
        }
    }
    public List<Upgrade> EnemiesUpgrades => _enemiesUpgrades;
    #endregion

    public void Initialize(LevelBuilder builderPrefab, EquipmentInventory equipmentInventory)
    {
        _equipmentUpgrades = new List<Upgrade>();

        PlayerBaseWeapon = null;

        if (equipmentInventory != null)
        {
            foreach(Equipment equipment in equipmentInventory.GetEquipment())
            {
                if (equipment != null)
                {
                    Upgrade upgrade = equipment.EquipUpgrade;

                    if (upgrade != null)
                    {
                        _equipmentUpgrades.Add(upgrade);
                    }

                    if (equipment as WeaponEquipment != null)
                    {
                        PlayerBaseWeapon = (equipment as WeaponEquipment).BaseWeapon;
                    }

                    _equipmentUpgrades.AddRange(equipment.RarityUpgrades);
                }
                else continue;
            }
        }

        LevelBuilder = Instantiate(builderPrefab);
        LevelBuilder.Construct(_levelEnvironment);
    }
}
