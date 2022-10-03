using System.Collections.Generic;

public interface IUpgradeable
{
    public Level Level { get; }

    public List<Upgrade> Upgrades { get; }

    public bool Upgrade(Upgrade upgrade);
}
