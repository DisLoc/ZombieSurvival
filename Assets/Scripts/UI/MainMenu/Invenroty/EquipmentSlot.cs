using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private Sprite _nullEquipmentBackground;

    [SerializeField] private Image _equipmentBackground;
    [SerializeField] private Image _equipmentIcon;
    [SerializeField] private Image _equipmentTypeIcon;
    [SerializeField] private Text _equipmentLevel;

    private Equipment _equipment;
    private EquipmentTypesData _equipmentTypesData;

    public Equipment Equipment => _equipment;

    public void Initialize(EquipmentTypesData typesData, Equipment equipment = null)
    {
        _equipmentTypesData = typesData;

        _equipment = equipment;

        UpdateSlot();
    }

    public bool SetSlot(Equipment equipment)
    {
        if (equipment == null)
        {
            if (_isDebug) Debug.Log("Equipment error!");

            return false;
        }

        _equipment = equipment;

        UpdateSlot();

        return true;
    }

    private void UpdateSlot()
    {
        if (_equipment == null)
        {
            _equipmentBackground.sprite = _nullEquipmentBackground;

            _equipmentIcon.enabled = false;
            _equipmentLevel.enabled = false;
            _equipmentTypeIcon.enabled = false;
        }
        else
        {
            _equipmentBackground.sprite = _equipmentTypesData[_equipment.EquipRarity].RarityBackground;

            _equipmentIcon.enabled = true;
            _equipmentLevel.enabled = true;
            _equipmentTypeIcon.enabled = true;

            _equipmentIcon.sprite = _equipment.Icon;
            _equipmentTypeIcon.sprite = _equipmentTypesData[_equipment.EquipSlot].SlotIcon;
            _equipmentLevel.text = ((int)_equipment.Level.Value).ToString();
        }
    }
}
