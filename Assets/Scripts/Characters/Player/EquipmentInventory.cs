using System.Collections.Generic;
using UnityEngine;

public class EquipmentInventory
{
    private Dictionary<EquipSlot, Equipment> _equipment;

    public EquipmentInventory()
    {
        _equipment = new Dictionary<EquipSlot, Equipment>();
    }
    
    public EquipmentInventory(List<Equipment> equipment)
    {
        _equipment = new Dictionary<EquipSlot, Equipment>();

        foreach (var item in equipment)
        {
            Add(item);
        }
    }

    public Equipment this[EquipSlot slot]
    {
        get
        {
            if (_equipment.ContainsKey(slot))
            {
                return _equipment[slot];
            }
            else return null;
        }
    }

    public bool Add(Equipment equipment)
    {
        if (!_equipment.ContainsKey(equipment.EquipSlot) || (_equipment.ContainsKey(equipment.EquipSlot) && _equipment[equipment.EquipSlot] != null))
        {
            equipment.Initialize();
            _equipment.Add(equipment.EquipSlot, equipment);

            return true;
        }

        return false;
    }
}
