using System.Collections.Generic;

public class UpgradeList
{
    private List<Upgrade> _upgrades;

    private float _upgradesValue;
    private float _upgradesMultiplier;

    public float UpgradesValue => _upgradesValue;
    public float UpgradesMultiplier => _upgradesMultiplier;

    public UpgradeList()
    {
        _upgrades = new List<Upgrade>();
    }

    public void Add(Upgrade upgrade)
    {
        _upgrades.Add(upgrade);

        CalculateUpgrades();
    }

    public bool Remove(Upgrade upgrade)
    {
        if (_upgrades.Remove(upgrade))
        {
            CalculateUpgrades();

            return true;
        }
        else return false;
    }

    private void CalculateUpgrades()
    {
        float value = 0, multiplier = 1;

        foreach(Upgrade upgrade in _upgrades)
        {
            value += upgrade.UpgradeValue;
            multiplier += (upgrade.UpgradeMultiplier - 1);
        }

        _upgradesValue = value;
        _upgradesMultiplier = multiplier;
    }
}
