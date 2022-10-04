using System.Collections.Generic;

public interface IUpgradeable
{
    public Level Level { get; }

    public UpgradeList Upgrades { get; }

    public bool Upgrade(Upgrade upgrade);
}
