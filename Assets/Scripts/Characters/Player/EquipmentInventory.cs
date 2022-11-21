using System.Collections.Generic;

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
        if (!_equipment.ContainsKey(equipment.EquipSlot) || (_equipment.ContainsKey(equipment.EquipSlot) && _equipment[equipment.EquipSlot] != null))
        {
            equipment.Initialize();
            _equipment.Add(equipment.EquipSlot, equipment);

            return true;
        }

        return false;
    }
}
