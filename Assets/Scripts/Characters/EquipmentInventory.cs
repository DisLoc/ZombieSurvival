using System.Collections.Generic;
using UnityEngine;

public class EquipmentInventory
{
    private Dictionary<EquipSlots, Equipment> _equipment;

    public EquipmentInventory()
    {
        _equipment = new Dictionary<EquipSlots, Equipment>();
    }
    
    public EquipmentInventory(List<Equipment> equipment)
    {
        _equipment = new Dictionary<EquipSlots, Equipment>();

        foreach (var item in equipment)
        {
            Add(item);
        }
    }

    public bool Add(Equipment equipment)
    {
        if (!_equipment.ContainsKey(equipment.EquipSlot) || (_equipment.ContainsKey(equipment.EquipSlot) && _equipment[equipment.EquipSlot] != null))
        {
            _equipment.Add(equipment.EquipSlot, equipment);
            return true;
        }

        return false;
    }
}
