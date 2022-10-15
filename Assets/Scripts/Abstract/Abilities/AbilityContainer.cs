using UnityEngine;

public abstract class AbilityContainer : MonoBehaviour, IUpgradeable
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    [Header("Settings")]
    protected UpgradeList _upgrades;

    public UpgradeList Upgrades => _upgrades;
    public abstract AbilityStats Stats { get; }
    public abstract AbilityUpgradeData UpgradeData { get; }

    public virtual void Initialize()
    {
        Stats.Initialize();
    }

    public virtual bool Upgrade(Upgrade upgrade)
    {
        Stats.GetUpgrade(upgrade);

        if (upgrade.IsAbilityUpgrade && upgrade.AbilityMarker.Equals(Stats.AbilityMarker))
        {
            Stats.Level.LevelUp();
            return true;
        }

        return false;
    }
}
