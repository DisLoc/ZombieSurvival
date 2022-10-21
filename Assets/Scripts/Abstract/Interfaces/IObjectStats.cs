public interface IObjectStats
{
    /// <summary>
    /// Stats initialization
    /// </summary>
    public void Initialize();

    /// <summary>
    /// Upgrade stats
    /// </summary>
    /// <param name="upgrade">Upgrade need to add</param>
    public void GetUpgrade(Upgrade upgrade);
}
