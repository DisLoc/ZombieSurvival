using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private Button _button;
    [SerializeField] private EquipSlot _validSlot;
    [SerializeField] private Sprite _nullEquipmentBackground;

    [SerializeField] private Image _equipmentBackground;
    [SerializeField] private Image _equipmentIcon;
    [SerializeField] private Image _equipmentTypeIcon;
    [SerializeField] private Text _equipmentLevel;

    private Equipment _equipment;
    private EquipmentTypesData _equipmentTypesData;

    public Button Button => _button;
    public EquipSlot ValidSlot => _validSlot;
    public Equipment Equipment => _equipment;

    public void Initialize(EquipmentTypesData typesData, Equipment equipment = null)
    {
        _equipmentTypesData = typesData;

        _equipment = equipment;

        UpdateSlot();
    }

    public bool SetSlot(Equipment equipment)
    {
        _equipment = equipment;

        UpdateSlot();

        return _equipment != null;
    }

    private void UpdateSlot()
    {
        _equipmentTypeIcon.enabled = true;
        _equipmentTypeIcon.sprite = _equipmentTypesData[_validSlot].SlotIcon;

        if (_equipment == null)
        {
            _equipmentBackground.sprite = _nullEquipmentBackground;

            _equipmentIcon.enabled = false;
            _equipmentLevel.enabled = false;
        }
        else
        {
            _equipmentBackground.sprite = _equipmentTypesData[_equipment.EquipRarity].RarityBackground;

            _equipmentIcon.enabled = true;
            _equipmentLevel.enabled = true;

            _equipmentIcon.sprite = _equipment.Icon;
            _equipmentTypeIcon.sprite = _equipmentTypesData[_equipment.EquipSlot].SlotIcon;
            _equipmentLevel.text = "LVL " + ((int)_equipment.Level.Value).ToString();
        }
    }
}
