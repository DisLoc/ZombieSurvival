using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipmentUpgrades
{
    [SerializeField] private List<EquipmentUpgrade> _upgrades;

    public UpgradeList GetUpgrades(int level)
    {
        UpgradeList upgrades = new UpgradeList();

        foreach (EquipmentUpgrade upgrade in _upgrades)
        {
            if (upgrade.RequiredLevel <= level && upgrade.UpgradeData != null)
            {
                upgrades.Add(upgrade.UpgradeData);
            }
        }

        return upgrades;
    }
}
