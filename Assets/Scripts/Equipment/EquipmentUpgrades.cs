using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipmentUpgrades
{
    [SerializeField] private List<EquipmentUpgrade> _upgrades;

    public UpgradeData GetUpgrade(int level)
    {
        return _upgrades.Find(item => item.RequiredLevel == level).UpgradeData;
    }
}
