public interface IUpgradeable
{
    public Level Lvl { get; }

    public void Upgrade(Upgrade upgrade);
}
