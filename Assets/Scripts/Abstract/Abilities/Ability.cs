public abstract class Ability : IUpgradeable
{
    protected Level _level;
    protected UpgradeList _upgrades;

    public Level Level => _level;
    public UpgradeList Upgrades => _upgrades;
    public abstract CurrentUpgrade CurrentUpgrade { get; }

    public virtual void Initialize()
    {

    }

    public virtual bool Upgrade(Upgrade upgrade)
    {
        throw new System.NotImplementedException();
    }
}
