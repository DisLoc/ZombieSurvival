using System.Collections.Generic;

public class EquipmentMaterialInventory : Inventory
{
    private Dictionary<EquipSlot, int> _materials;

    public EquipmentMaterialInventory()
    {
        _materials = new Dictionary<EquipSlot, int>();

        _materials.Add(EquipSlot.Helmet, 0);
        _materials.Add(EquipSlot.Armor, 0);
        _materials.Add(EquipSlot.Boots, 0);
        _materials.Add(EquipSlot.Weapon, 0);
        _materials.Add(EquipSlot.Hands, 0);
        _materials.Add(EquipSlot.Belt, 0);
    }

    public EquipmentMaterialInventory(Dictionary<EquipSlot, int> materials)
    {
        _materials = materials;
    }

    public int this[EquipSlot slot]
    {
        get
        {
            if (!_materials.ContainsKey(slot))
            {
                return -1;
            }
            else return _materials[slot];
        }
    }

    public void Add(EquipSlot materialSlot, int count)
    {
        if (!_materials.ContainsKey(materialSlot))
        {
            _materials.Add(materialSlot, 0);
        }

        _materials[materialSlot] += count;
    }

    public bool Remove(EquipSlot materialSlot, int requiredAmount)
    {
        if (IsEnough(materialSlot, requiredAmount))
        {
            _materials[materialSlot] -= requiredAmount;

            return true;
        }
        else return false;
    }

    public bool IsEnough(EquipSlot materialSlot, int requiredAmount)
    {
        if (!_materials.ContainsKey(materialSlot))
        {
            return false;
        }

        return _materials[materialSlot] >= requiredAmount;
    }

    #region Serialization
    public override SerializableData SaveData()
    {
        EquipmentMaterialsData data = new EquipmentMaterialsData();

        data.Materials = _materials;

        return data;
    }

    public override void LoadData(SerializableData data)
    {
        if (data == null) return;

        if (data is EquipmentMaterialsData equipmentData)
        {
            _materials = new Dictionary<EquipSlot, int>();

            _materials.Add(EquipSlot.Helmet, equipmentData.Materials[EquipSlot.Helmet]);
            _materials.Add(EquipSlot.Armor, equipmentData.Materials[EquipSlot.Armor]);
            _materials.Add(EquipSlot.Boots, equipmentData.Materials[EquipSlot.Boots]);
            _materials.Add(EquipSlot.Weapon, equipmentData.Materials[EquipSlot.Weapon]);
            _materials.Add(EquipSlot.Hands, equipmentData.Materials[EquipSlot.Hands]);
            _materials.Add(EquipSlot.Belt, equipmentData.Materials[EquipSlot.Belt]);
        }
    }

    public override void ResetData()
    {
        return;
    }

    [System.Serializable]
    private class EquipmentMaterialsData : SerializableData
    {
        public Dictionary<EquipSlot, int> Materials;
        public List<KeyValuePair<EquipSlot, int>> materials;

        public EquipmentMaterialsData()
        {
            Materials = new Dictionary<EquipSlot, int>();
            materials = new List<KeyValuePair<EquipSlot, int>>();
        }

        public void Add(KeyValuePair<EquipSlot, int> pair)
        {
            materials.Add(pair);
        }
    }
    #endregion
}
