[System.Serializable]
public struct Level : IStat
{
    public int Value { get; }

    public Level Lvl => this;

    public void Upgrade(Upgrade upgrade)
    {

    }
}
