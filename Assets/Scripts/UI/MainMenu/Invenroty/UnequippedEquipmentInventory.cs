using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnequippedEquipmentInventory
{
    private EquipmentSlot _slotPrefab;
    private Transform _equipmentParent;

    private InventoryMenu _menu;
    private List<Equipment> _equipment;
    private List<EquipmentSlot> _slots;

    public List<Equipment> Equipment => _equipment;

    public UnequippedEquipmentInventory(EquipmentSlot slotPrefab, Transform equipmentParent, InventoryMenu menu)
    {
        _slotPrefab = slotPrefab;
        _equipmentParent = equipmentParent;
        _menu = menu;

        _equipment = new List<Equipment>();
        _slots = new List<EquipmentSlot>();
    }

    public void AddEquipment(Equipment equipment)
    {
        if (equipment != null)
        {
            EquipmentSlot slot = Object.Instantiate(_slotPrefab, _equipmentParent);

            slot.Initialize(_menu.EquipmentTypesData, equipment);
            slot.Button.onClick.AddListener(new UnityEngine.Events.UnityAction(() => _menu.OnItemClick(slot)));

            _equipment.Add(equipment);
            _slots.Add(slot);

            UpdateInventory();
        }
    }

    public bool RemoveEquipment(Equipment equipment)
    {
        if (equipment != null)
        {
            EquipmentSlot slot = _slots.Find(item => item.Equipment.Equals(equipment));

            if (slot == null)
            {
                return false;
            }

            _slots.Remove(slot);
            Object.Destroy(slot.gameObject);

            bool removed = _equipment.Remove(equipment);
           
            if (removed) 
            {
                UpdateInventory();
            }

            return removed;
        }
        else return false;
    }
    
    public void UpdateInventory()
    {
        foreach(var slot in _slots)
        {
            if (slot.Equipment != null)
            {
                slot.SetSlot(slot.Equipment);
            }
            else
            {
                Object.Destroy(slot);
            }
        }
    }
}
