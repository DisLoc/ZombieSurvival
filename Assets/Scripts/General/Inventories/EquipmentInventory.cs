using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipmentInventory : Inventory
{
    [SerializeField] private Transform _parent;
    [SerializeField] private EquipmentList _equipmentList;

    private List<Equipment> _equipment;

    public EquipmentList EquipmentList => _equipmentList;
    public List<Equipment> Equipment => _equipment;

    public void Initialize()
    {
        _equipment = new List<Equipment>();
    }

    public void Add(Equipment equipment)
    {
        Equipment newEquipment = Object.Instantiate(equipment);

        newEquipment.Initialize();

        _equipment.Add(newEquipment);
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
                Object.Destroy(removingEquipment);

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
                    Object.Destroy(removingEquipments[i]);
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

        foreach(Equipment equipment in _equipment)
        {
            data.Add(equipment);
        }

        return data;
    }

    public override void LoadData(SerializableData data)
    {
        if (data == null) return;

        if (data is EquipmentInventoryData equipmentData)
        {                
            for (int i = 0; i < equipmentData.equipments.Count; i++)
            {
                Equipment equipment = _equipmentList[equipmentData[i].id];

                if (equipment != null)
                {
                    Equipment loadedEquipment = Object.Instantiate(equipment, _parent);

                    loadedEquipment.Initialize();
                    loadedEquipment.Level.SetValue(equipmentData[i].level);
                    loadedEquipment.isEquiped = equipmentData[i].isEquipped;

                    _equipment.Add(loadedEquipment);
                }
            }
        }
    }

    public override void ResetData()
    {
        if (_equipment == null) return;

        foreach(Equipment equipment in _equipment)
        {
            Object.Destroy(equipment.gameObject);
        }

        _equipment = new List<Equipment>();
    }

    [System.Serializable]
    private class EquipmentInventoryData : SerializableData
    {
        public List<SingleEquipmentData> equipments;

        public EquipmentInventoryData()
        {
            equipments = new List<SingleEquipmentData>();
        }

        public SingleEquipmentData this[int index]
        {
            get
            {
                if (index > equipments.Count)
                {
                    return null;
                }
                else return equipments[index];
            }
        }

        public void Add(Equipment equipment)
        {
            SingleEquipmentData data = new SingleEquipmentData();

            data.id = equipment.ID;
            data.level = (int)equipment.Level.Value;
            data.isEquipped = equipment.isEquiped;

            equipments.Add(data);
        }

        [System.Serializable]
        public class SingleEquipmentData
        {
            public int id;
            public int level;
            public bool isEquipped;
        }
    }
    #endregion
}
