[System.Serializable]
public struct Level : IStat, IUpgradeable
{
    public int Value { get; }

    public Level Lvl => this;

    public void Upgrade(Upgrade upgrade)
    {

    }
}
