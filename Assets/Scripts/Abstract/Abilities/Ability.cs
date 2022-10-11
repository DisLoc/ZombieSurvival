using UnityEngine;

public abstract class Ability : IUpgradeable
{
    [SerializeField] protected string _name;
    [SerializeField] protected Sprite _inventoryIcon;
    [SerializeField] protected Sprite _upgradeIcon;

    protected UpgradeList _upgrades;

    public string Name => _name;
    public UpgradeList Upgrades => _upgrades;
    public Sprite InventoryIcon => _inventoryIcon;
    public Sprite UpgradeIcon => _upgradeIcon;
    public abstract AbilityStats Stats { get; }
    public abstract CurrentUpgrade CurrentUpgrade { get; }

    public virtual void Initialize()
    {

    }

    public virtual bool Upgrade(Upgrade upgrade)
    {
        int level = (int)Stats.Level.Value;

        Stats.GetUpgrade(upgrade);

        return level == (int)Stats.Level.Value;
    }
}
