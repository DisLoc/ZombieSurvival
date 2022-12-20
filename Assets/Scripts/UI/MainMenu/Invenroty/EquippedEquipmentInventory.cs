using System.Collections.Generic;

public class EquippedEquipmentInventory
{
    private Dictionary<EquipSlot, Equipment> _equipment;

    public EquippedEquipmentInventory()
    {
        _equipment = new Dictionary<EquipSlot, Equipment>();
    }
    
    public EquippedEquipmentInventory(List<Equipment> equipment)
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
    
    public List<Equipment> GetEquipment()
    {
        List<Equipment> equipment = new List<Equipment>();

        foreach(KeyValuePair<EquipSlot, Equipment> item in _equipment)
        {
            if (item.Value != null) equipment.Add(item.Value);
        }

        return equipment;
    }

    public bool Add(Equipment equipment)
    {
        if (!_equipment.ContainsKey(equipment.EquipSlot))
        {
            _equipment.Add(equipment.EquipSlot, null);
        }

        if (_equipment[equipment.EquipSlot] == null)
        {
            _equipment[equipment.EquipSlot] = equipment;

            return true;
        }

        return false;
    }

    public bool Remove(Equipment equipment)
    {
        if (_equipment.ContainsKey(equipment.EquipSlot))
        {
            _equipment[equipment.EquipSlot] = null;

            return true;
        }

        return false;
    }

    public bool Contains(Equipment equipment)
    {
        return _equipment.ContainsKey(equipment.EquipSlot) && equipment.Equals(_equipment[equipment.EquipSlot]);
    }
}
