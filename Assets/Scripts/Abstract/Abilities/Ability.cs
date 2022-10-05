
public abstract class Ability : IUpgradeable
{
    public Level Level { get; }
    public UpgradeList Upgrades { get; }

    public virtual bool Upgrade(Upgrade upgrade)
    {
        throw new System.NotImplementedException();
    }
}