using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] protected Level _level;
    [SerializeField] protected EquipmentData _equipmentData;
    [SerializeField] protected int _itemID;

    [HideInInspector] 
    public bool isEquiped;

    public EquipmentData EquipmentData => _equipmentData;
    public int ID
    {
        get { return _itemID; }
        set { _itemID = value; }
    }

    public Sprite Icon => _equipmentData.Icon;
    public EquipSlot EquipSlot => _equipmentData.EquipSlot;
    public EquipRarity EquipRarity => _equipmentData.EquipRarity;
    public UpgradingStat UpgradingStat => _equipmentData.UpgradingStat;

    public Upgrade EquipUpgrade => new Upgrade(_equipmentData.EquipmentUpgrades.GetUpgrade((int)_level.Value).UpgradeData);
    public List<Upgrade> RarityUpgrades
    {
        get
        {
            List<Upgrade> upgrades = new List<Upgrade>();
            EquipmentData equipmentData = _equipmentData;

            while (equipmentData.PreviousRarityEquipment != null)
            {
                upgrades.Add(equipmentData.RarityUpgrade);
                equipmentData = equipmentData.PreviousRarityEquipment;
            }

            return upgrades;
        }
    }

    public Upgrade StatsUpgrade => new Upgrade(_equipmentData.EquipmentUpgrades.GetUpgrade((int)_level.Value).UpgradeData);
    public EquipmentUpgrade CurrentUpgrade => _level.Value != _level.MaxValue ? _equipmentData.EquipmentUpgrades.GetUpgrade((int)_level.Value + 1) : null;

    public Level Level => _level;

    public void Initialize()
    {
        _level.Initialize();
    }
}
