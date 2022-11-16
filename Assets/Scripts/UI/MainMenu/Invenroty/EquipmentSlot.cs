using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private EquipSlots _equipmentType;
    [SerializeField] private Sprite _equipmentTypeIcon;

    private Equipment _equipment;

    public Equipment Equipment => _equipment;

    public void SetSlot(Equipment equipment)
    {
        _equipment = equipment;

        UpdateSlot();
    }

    private void UpdateSlot()
    {

    }
}
