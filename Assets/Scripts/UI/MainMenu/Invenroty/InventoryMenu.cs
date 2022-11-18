using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class InventoryMenu : UIMenu
{
    [Header("Inventory menu settings")]
    [SerializeField] private ItemUpgradeMenu _upgradeMenu;

    [Header("Equipment settings")]
    [SerializeField] private Text _damageText;
    [SerializeField] private Text _healthText;

    [SerializeField] private List<EquipmentSlot> _slots;

    [SerializeField] private EquipmentTypesData _equipmentTypesData;

    [SerializeField] private Player _player;

    [SerializeField] private List<Equipment> _baseEquipment;

    private EquipmentInventory _equipmentInventory;

    public EquipmentTypesData EquipmentTypesData => _equipmentTypesData;

    public override void Initialize(MainMenu mainMenu, UIMenu parentMenu = null)
    {
        base.Initialize(mainMenu, parentMenu);

        _equipmentInventory = new EquipmentInventory(_baseEquipment);

        _upgradeMenu.Initialize(mainMenu, this);

        foreach(EquipmentSlot slot in _slots)
        {
            slot.Initialize(_equipmentTypesData);

            Equipment equipment = _equipmentInventory[slot.ValidSlot];

            if (equipment != null)
            {
                slot.SetSlot(equipment);
            }
        }
    }

    public override void Display(bool playAnimation = false)
    {
        _upgradeMenu.Hide();

        base.Display(playAnimation);

        UpdateValues();
    }

    public override void Hide(bool playAnimation = false)
    {
        _upgradeMenu.Hide();

        base.Hide(playAnimation);
    }

    public void OnItemClick(EquipmentSlot slot)
    {
        if (slot.Equipment == null)
        {
            if (_isDebug) Debug.Log("Missing equipment");

            return;
        }

        _upgradeMenu.SetEquipment(slot.Equipment);
        _upgradeMenu.Display(true);
    }

    public void UpdateValues()
    {
        int totalDamage = (int)(_player.Stats as PlayerStats).Damage.BaseValue;
        int totalHP = (int)_player.Stats.Health.MaxHealthPoints.BaseValue;

        foreach(EquipmentSlot slot in _slots)
        {
            if (slot.Equipment != null)
            {
                if (slot.Equipment.UpgradingStat.Equals(UpgradedStat.Damage))
                {
                    foreach (UpgradeData data in slot.Equipment.EquipUpgrade.Upgrades)
                    {
                        totalDamage += (int)data.UpgradeValue;
                    }
                }

                else if (slot.Equipment.UpgradingStat.Equals(UpgradedStat.Health))
                {
                    foreach (UpgradeData data in slot.Equipment.EquipUpgrade.Upgrades)
                    {
                        totalHP += (int)data.UpgradeValue;
                    }
                }

                else if (_isDebug) Debug.Log("Equipment upgrades error!");
            }
        }

        _damageText.text = totalDamage.ToString();
        _healthText.text = totalHP.ToString();
    }
}
