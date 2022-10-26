using UnityEngine;

public abstract class AbilityContainer : MonoBehaviour, IUpgradeable
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    [Header("Settings")]
    [SerializeField] protected string _name;
    [Tooltip("Icon displays in inventory and on choice")]
    [SerializeField] protected Sprite _icon;

    protected UpgradeList _upgrades;

    public string Name => _name;
    public UpgradeList Upgrades => _upgrades;
    /// <summary>
    /// Return false if ability not reached MaxLevel or MaxLevel is infinite
    /// </summary>
    public bool IsMaxLevel => !Stats.Level.MaxValueIsInfinite && Stats.Level.Value == Stats.Level.MaxValue;
    public Sprite Icon => _icon;
    /// <summary>
    /// Current upgrade of this ability
    /// </summary>
    public abstract CurrentUpgrade CurrentUpgrade { get; }
    /// <summary>
    /// Stats of this ability
    /// </summary>
    public abstract AbilityStats Stats { get; }
    /// <summary>
    /// All ability upgrades
    /// </summary>
    public abstract AbilityUpgradeData UpgradeData { get; }

    public virtual void Initialize()
    {
        Stats.Initialize();

        _upgrades = new UpgradeList();
    }

    /// <summary>
    /// Upgrade ability
    /// </summary>
    /// <param name="upgrade"></param>
    /// <returns>Return true if level up</returns>
    public virtual bool Upgrade(Upgrade upgrade)
    {
        if (upgrade.IsAbilityUpgrade && upgrade.AbilityMarker.Equals(Stats.AbilityMarker) && upgrade.Equals(CurrentUpgrade.Upgrade))
        {
            foreach (UpgradeData data in upgrade.Upgrades)
            {
                _upgrades.Add(data);
            }

            Stats.Level.LevelUp();
            
            return true;
        }

        return false;
    }
}
