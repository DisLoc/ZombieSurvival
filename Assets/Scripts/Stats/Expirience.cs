[System.Serializable]
public struct Expirience : IStat
{
    public int Value { get; }

    private Level _level;
    public Level Lvl => _level;

    public void Upgrade(Upgrade upgrade)
    {

    }
}
