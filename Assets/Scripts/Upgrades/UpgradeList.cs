using System.Collections.Generic;

public class UpgradeList
{
    private List<UpgradeData> _upgrades;

    private float _upgradesValue;
    private float _upgradesMultiplier;

    public List<UpgradeData> Upgrades => _upgrades;
    /// <summary>
    /// Total value of the improvements
    /// </summary>
    public float UpgradesValue => _upgradesValue;
    /// <summary>
    /// Total multiplier of the improvements
    /// </summary>
    public float UpgradesMultiplier => _upgradesMultiplier;

    public UpgradeList()
    {
        _upgrades = new List<UpgradeData>();

        _upgradesValue = 0;
        _upgradesMultiplier = 1;
    }

    /// <summary>
    /// Add new upgrade
    /// </summary>
    /// <param name="upgrade"></param>
    public void Add(UpgradeData upgrade)
    {
        _upgrades.Add(upgrade);

        CalculateUpgrades();
    }

    /// <summary>
    /// Dispel one upgrade
    /// </summary>
    /// <param name="upgrade">Upgrade need to dispel</param>
    /// <returns>Dispel is done</returns>
    public bool Dispel(UpgradeData upgrade)
    {
        if (_upgrades.Remove(upgrade))
        {
            CalculateUpgrades();

            return true;
        }
        else return false;
    }

    /// <summary>
    /// Dispel all upgrades
    /// </summary>
    public void DispelAll()
    {
        _upgrades.Clear();

        CalculateUpgrades();
    }

    /// <summary>
    /// Calculate total upgrades values
    /// </summary>
    private void CalculateUpgrades()
    {
        float value = 0, multiplier = 1;

        foreach(UpgradeData upgrade in _upgrades)
        {
            value += upgrade.UpgradeValue;
            multiplier += (upgrade.UpgradeMultiplier - 1);
        }

        _upgradesValue = value;
        _upgradesMultiplier = multiplier;
    }
}
