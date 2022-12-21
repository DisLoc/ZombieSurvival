using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CampInventory : Inventory
{
    [SerializeField] private List<CampUpgrade> _campUpgrades;
    [SerializeField] private List<Talent> _talents;

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

            foreach (var talent in _talents.FindAll(item => item.Unlocked == true))
            {
                Upgrade current = talent.Upgrade;
                
                if (current != null)
                {
                    upgrades.Add(current);
                }
            }

            return upgrades;
        }
    }

    /// <summary>
    /// All available camp upgrades
    /// </summary>
    public List<CampUpgrade> Upgrades => _campUpgrades;
    /// <summary>
    /// All available camp talents
    /// </summary>
    public List<Talent> Talents => _talents;

    public void Initialize()
    {
        foreach(var upgrade in _campUpgrades)
        {
            upgrade.Initialize();
        }
        
        foreach(var talent in _talents)
        {
            talent.Initialize(false);
        }
    }

    public void UpdateButtons()
    {
        foreach (var upgrade in _campUpgrades)
        {
            upgrade.UpdateValues();
        }
    }

    public override SerializableData SaveData()
    {
        CampUpgradeData data = new CampUpgradeData();

        foreach(var upgrade in _campUpgrades)
        {
            data.Add(upgrade);
        }
        
        foreach(var talent in _talents)
        {
            data.Add(talent);
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
            
            if (loadedData.talents.Count != _talents.Count)
            {
                Debug.Log("Camp inventory load error!");
            }
            else
            {
                for (int i = 0; i < _talents.Count; i++)
                {
                    _talents[i].Initialize(loadedData.talents[i].unlocked);
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
        public List<ConcreteTalentData> talents;

        public CampUpgradeData() 
        {
            upgrades = new List<ConcreteUpgradeData>();
            talents = new List<ConcreteTalentData>();
        }

        public void Add(CampUpgrade upgrade)
        {
            ConcreteUpgradeData data = new ConcreteUpgradeData();

            data.level = (int)upgrade.Level.Value;

            upgrades.Add(data);
        }
        
        public void Add(Talent talent)
        {
            ConcreteTalentData data = new ConcreteTalentData();

            data.unlocked = talent.Unlocked;

            talents.Add(data);
        }

        [System.Serializable]
        public class ConcreteUpgradeData
        {
            public int level;
        }
        
        [System.Serializable]
        public class ConcreteTalentData
        {
            public bool unlocked;
        }
    }
}
