using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipmentInventory : Inventory
{
    [SerializeField] private EquipmentList _equipmentList;

    private List<Equipment> _equipment;

    public void Add(Equipment equipment)
    {
        _equipment.Add(equipment);
    }

    public bool Spend(Equipment equipment, int count = 1)
    {
        if (count < 1) return false;

        if (count == 1)
        {
            Equipment removingEquipment = _equipment.Find(item => item.EquipmentData.Equals(equipment.EquipmentData));

            if (removingEquipment != null)
            {
                _equipment.Remove(removingEquipment);

                return true;
            }
            else return false;
        }
        else
        {
            List<Equipment> removingEquipments;

            if (IsEnough(equipment, count, out removingEquipments))
            {
                for(int i = 0; i < count; i++)
                {
                    _equipment.Remove(removingEquipments[i]);
                }

                return true;
            }
        }

        return false;
    }

    public bool IsEnough(Equipment equipment, int count = 1)
    {
        return _equipment.FindAll(item => item.EquipmentData.Equals(equipment.EquipmentData)).Count >= count;
    }

    public bool IsEnough(EquipmentData data, int count = 1)
    {
        return _equipment.FindAll(item => item.EquipmentData.Equals(data)).Count >= count;
    }
    
    private bool IsEnough(Equipment equipment, int count, out List<Equipment> findedEquipment)
    {
        findedEquipment = _equipment.FindAll(item => item.EquipmentData.Equals(equipment.EquipmentData));

        return findedEquipment.Count >= count;
    }

    #region Serialization
    public override SerializableData SaveData()
    {
        EquipmentInventoryData data = new EquipmentInventoryData();

        return data;
    }

    public override void LoadData(SerializableData data)
    {

    }

    public override void ResetData()
    {
        
    }

    [System.Serializable]
    private class EquipmentInventoryData : SerializableData
    {

    }
    #endregion
}
