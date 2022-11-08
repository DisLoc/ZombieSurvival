using System.Collections.Generic;
using UnityEngine;

public class EquipmentInventory
{
    private Dictionary<EquipSlots, Equipment> _equipment;

    public EquipmentInventory()
    {
        _equipment = new Dictionary<EquipSlots, Equipment>();
    }
    
    public EquipmentInventory(Dictionary<EquipSlots, Equipment> equipment)
    {
        _equipment = new Dictionary<EquipSlots, Equipment>();


    }

    public bool Add(Equipment equipment)
    {
        return true;
    }
}
