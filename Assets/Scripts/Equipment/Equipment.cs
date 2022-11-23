using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] private Level _level;
    [SerializeField] private EquipmentData _equipmentData;

    public Sprite Icon => _equipmentData.Icon;
    public EquipSlot EquipSlot => _equipmentData.EquipSlot;
    public EquipRarity EquipRarity => _equipmentData.EquipRarity;
    public UpgradingStat UpgradingStat => _equipmentData.UpgradingStat;
    public Upgrade EquipUpgrade
    {
        get
        {
            if (_equipmentData.PreviousRarityEquipment != null)
            {
                return _equipmentData.PreviousRarityEquipment.RarityUpgrade + 
                    _equipmentData.RarityUpgrade + _equipmentData.EquipmentUpgrades.GetUpgrade((int)_level.Value);
            }
            else
            {
                return _equipmentData.RarityUpgrade + _equipmentData.EquipmentUpgrades.GetUpgrade((int)_level.Value);
            }
        }
    }

    public Level Level => _level;

    public bool isEquiped;

    public void Initialize()
    {
        _level.Initialize();
    }
}
