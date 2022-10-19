using UnityEngine;

public abstract class AbilityContainer : MonoBehaviour, IUpgradeable
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    [Header("Settings")]
    [SerializeField] protected string _name;

    protected UpgradeList _upgrades;

    public string Name => _name;
    public UpgradeList Upgrades => _upgrades;
    public bool IsMaxLevel => !Stats.Level.MaxValueIsInfinite && Stats.Level.Value == Stats.Level.MaxValue;
    public abstract CurrentUpgrade CurrentUpgrade { get; }
    public abstract AbilityStats Stats { get; }
    public abstract AbilityUpgradeData UpgradeData { get; }

    public virtual void Initialize()
    {
        Stats.Initialize();

        _upgrades = new UpgradeList();
    }

    public virtual bool Upgrade(Upgrade upgrade)
    {
        if (upgrade.IsAbilityUpgrade && upgrade.AbilityMarker.Equals(Stats.AbilityMarker))
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
