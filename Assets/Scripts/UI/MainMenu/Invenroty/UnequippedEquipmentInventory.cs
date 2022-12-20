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

            bool removed = _equipment.Remove(equipment);

            if (removed)
            {
                slot.Button.onClick.RemoveAllListeners();

                _slots.Remove(slot);
                Object.Destroy(slot.gameObject);

                UpdateInventory();
            }

            return removed;
        }
        else return false;
    }

    public void Cleanup()
    {
        _equipment.RemoveAll(item => item == null);

        for (int i = 0; i < _slots.Count; i++)
        {
            if (_slots[i] != null && (_slots[i].Equipment == null || !_equipment.Contains(_slots[i].Equipment)))
            {
                Object.Destroy(_slots[i].gameObject);

                _slots[i] = null;
            }
        }

        _slots.RemoveAll(item => item == null);
    }
    
    public void UpdateInventory()
    {
        Cleanup();

        if (_slots.Count != _equipment.Count)
        {
            foreach(var slot in _slots)
            {
                Object.Destroy(slot.gameObject);
            }

            _slots.Clear();

            foreach(var equipment in _equipment)
            {
                EquipmentSlot slot = Object.Instantiate(_slotPrefab, _equipmentParent);

                slot.Initialize(_menu.EquipmentTypesData, equipment);
                slot.Button.onClick.AddListener(new UnityEngine.Events.UnityAction(() => _menu.OnItemClick(slot)));

                _slots.Add(slot);
            }
        }
        else
        {
            foreach(var slot in _slots)
            {
                slot.SetSlot(slot.Equipment);
            }
        }
        
        Sort();
    }

    public void Sort() // bubble sort...
    {
        // TODO normal sorting

        List<Equipment> equipment = new List<Equipment>();

        for (int i = 0; i < _slots.Count; i++)
        {
            equipment.Add(_slots[i].Equipment);
        }

        for (int i = _slots.Count - 1; i > 0; i--)
        {
            for (int j = i; j > 0; j--)
            {
                if (equipment[j].EquipRarity > equipment[j - 1].EquipRarity)
                {
                    (equipment[j - 1], equipment[j]) = (equipment[j], equipment[j - 1]);
                }
            }
        }

        for(int i = 0; i < _slots.Count; i++)
        {
            _slots[i].SetSlot(equipment[i]);
        }
    }

    public bool Contains(Equipment equipment)
    {
        return _equipment.Find(item => equipment.Equals(item)) != null;
    }
}
