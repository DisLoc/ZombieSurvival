using UnityEngine;

public abstract class Ability : IUpgradeable
{
    [SerializeField] protected Sprite _inventoryIcon;
    [SerializeField] protected Sprite _upgradeIcon;

    protected UpgradeList _upgrades;

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
        throw new System.NotImplementedException();
    }
}
