using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class InventoryMenu : UIMenu
{
    [Header("Inventory menu settings")]
    [SerializeField] private ItemUpgradeMenu _upgradeMenu;
    [SerializeField] private LevelContextInstaller _contextInstaller;
    [SerializeField] private RectTransform _unequipedInventoryTransform;
    [SerializeField] private GridLayoutGroup _grid;

    [Header("Equipment settings")]
    [SerializeField] private EquipmentTypesData _equipmentTypesData;

    [SerializeField] private List<EquipmentSlot> _slots;
    [SerializeField] private Text _damageText;
    [SerializeField] private Text _healthText;

    [SerializeField] private EquipmentInventoryMenu _unequippedInventory; 

    [Header("Test")]
    [SerializeField] private Player _player;
    [SerializeField] private bool _useBaseEquipment;
    [SerializeField] private List<Equipment> _baseEquipment;

    private EquipmentInventory _equipmentInventory;

    public EquipmentTypesData EquipmentTypesData => _equipmentTypesData;

    public override void Initialize(MainMenu mainMenu, UIMenu parentMenu = null)
    {
        base.Initialize(mainMenu, parentMenu);

        _upgradeMenu.Initialize(mainMenu, this);

        _unequippedInventory.Initialize(this);
        _equipmentInventory = new EquipmentInventory();

        foreach (EquipmentSlot slot in _slots)
        {
            slot.Initialize(_equipmentTypesData);
        }

        if (_useBaseEquipment)
        {
            foreach (Equipment equipment in _baseEquipment)
            {
                Equip(equipment);
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
        int totalDamage = (int)_player.LevelUpgrades.GetUpgrade(1).DamageData.UpgradeValue;
        int totalHP = (int)_player.Stats.Health.MaxHealthPoints.BaseValue + (int)_player.LevelUpgrades.GetUpgrade(1).HealthData.UpgradeValue;

        foreach(EquipmentSlot slot in _slots)
        {
            if (slot.Equipment != null)
            {
                slot.SetSlot(slot.Equipment);

                if (slot.Equipment.UpgradingStat.Equals(UpgradingStat.Damage))
                {
                    foreach (UpgradeData data in slot.Equipment.EquipUpgrade.Upgrades)
                    {
                        totalDamage += (int)data.UpgradeValue;
                    }
                }

                else if (slot.Equipment.UpgradingStat.Equals(UpgradingStat.Health))
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

        _unequippedInventory.UpdateInventory();
    }

    public void Equip(Equipment equipment)
    {
        if (equipment == null) return;

        EquipmentSlot slot = _slots.Find(item => item.ValidSlot.Equals(equipment.EquipSlot));

        if (slot.Equipment != null)
        {
            _unequippedInventory.AddEquipment(slot.Equipment);
        }

        equipment.isEquiped = true;

        _equipmentInventory.Add(equipment);
        _unequippedInventory.RemoveEquipment(equipment);
        slot.SetSlot(equipment);

        _unequipedInventoryTransform.sizeDelta = new Vector2(0, GetInventoryHeight());

        Display();

        OnInventoryChange();
    }

    public void Unequip(Equipment equipment)
    {
        if (equipment == null) return;

        EquipmentSlot slot = _slots.Find(item => item.Equipment != null && item.Equipment.Equals(equipment));

        if (slot != null)
        {
            _unequippedInventory.AddEquipment(equipment);
            _equipmentInventory.Remove(equipment);

            equipment.isEquiped = false;

            slot.SetSlot(null);

            _unequipedInventoryTransform.sizeDelta = new Vector2(0, GetInventoryHeight());

            Display();

            OnInventoryChange();
        }
        else return;
    }
    
    private int GetInventoryHeight()
    {
        int rows = _unequippedInventory.Equipment.Count / 6 + (_unequippedInventory.Equipment.Count % 6 > 0 ? 1 : 0);

        return _grid.padding.top + _grid.padding.bottom + (int)(_grid.cellSize.y * rows) + (int)(_grid.spacing.y * rows);
    }

    public void OnInventoryChange()
    {
        _contextInstaller.SetEquipment(_equipmentInventory);
    }
}
