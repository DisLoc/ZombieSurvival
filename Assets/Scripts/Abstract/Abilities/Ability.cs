using UnityEngine;

public abstract class Ability : IUpgradeable
{
    [SerializeField] protected Sprite _icon;

    protected Level _level;
    protected UpgradeList _upgrades;

    public Level Level => _level;
    public UpgradeList Upgrades => _upgrades;
    public abstract CurrentUpgrade CurrentUpgrade { get; }
    public Sprite Icon => _icon;

    public virtual void Initialize()
    {

    }

    public virtual bool Upgrade(Upgrade upgrade)
    {
        throw new System.NotImplementedException();
    }
}
