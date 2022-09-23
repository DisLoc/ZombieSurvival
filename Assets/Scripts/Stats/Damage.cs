[System.Serializable]
public struct Damage : IStat, IUpgradeable
{
    public int Value { get; }

    private Level _level;
    public Level Lvl => _level;

    public void Upgrade(Upgrade upgrade)
    {

    }
}
