[System.Serializable]
public struct ProjectileSpeed : IStat
{
    private int _projectileSpeed;
    public int Value { get; }

    private Level _level;
    public Level Lvl => _level; 

    public void Upgrade(Upgrade upgrade)
    {

    }
}
