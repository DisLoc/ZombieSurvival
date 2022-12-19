using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CampInventory : Inventory
{
    [SerializeField] private List<CampUpgrade> _campUpgrades;

    public List<Upgrade> CampUpgrades
    {
        get
        {
            List<Upgrade> upgrades = new List<Upgrade>();

            foreach (var upgrade in _campUpgrades)
            {
                Upgrade current = upgrade.CurrentUpgrade;
                
                if (current != null)
                {
                    upgrades.Add(current);
                }
            }

            return upgrades;
        }
    }

    public void Initialize()
    {
        foreach(var upgrade in _campUpgrades)
        {
            upgrade.Initialize();
        }
    }

    public void Upgrade(CampUpgrade upgrade)
    {
        _campUpgrades.Find(item => item.Equals(upgrade)).Upgrade();
    }

    public override SerializableData SaveData()
    {
        CampUpgradeData data = new CampUpgradeData();

        foreach(var upgrade in _campUpgrades)
        {
            data.Add(upgrade);
        }

        return data;
    }

    public override void LoadData(SerializableData data)
    {
        if (data is CampUpgradeData loadedData)
        {
            if (loadedData.upgrades.Count != _campUpgrades.Count)
            {
                Debug.Log("Camp inventory load error!");
            }
            else
            {
                for (int i = 0; i < _campUpgrades.Count; i++)
                {
                    _campUpgrades[i].Level.SetValue(loadedData.upgrades[i].level);
                    _campUpgrades[i].UpdateValues();
                }
            }
        }
        else return;
    }

    public override void ResetData()
    {
        Initialize();
    }

    [System.Serializable]
    private class CampUpgradeData : SerializableData
    {
        public List<ConcreteUpgradeData> upgrades;

        public CampUpgradeData() 
        {
            upgrades = new List<ConcreteUpgradeData>();
        }

        public void Add(CampUpgrade upgrade)
        {
            ConcreteUpgradeData data = new ConcreteUpgradeData();

            data.level = (int)upgrade.Level.Value;

            upgrades.Add(data);
        }

        [System.Serializable]
        public class ConcreteUpgradeData
        {
            public int level;

        }
    }
}
