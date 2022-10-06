using System.Collections.Generic;

public interface IUpgradeable
{
    public UpgradeList Upgrades { get; }

    public bool Upgrade(Upgrade upgrade);
}
