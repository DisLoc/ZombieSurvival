using UnityEngine;
using UnityEngine.UI;

public class ItemUpgradeMenu : UIMenu
{
    [Header("Upgrade menu settings")]
    [SerializeField] private ItemResetMenu _resetMenu;
    [SerializeField] private Sprite _damageIcon;
    [SerializeField] private Sprite _healthIcon;

    [Header("Equipment info menu")]
    [SerializeField] private Text _rarityText;
    [SerializeField] private Image _rarityBackground;
    [SerializeField] private Image _equipmentIcon;
    [SerializeField] private Image _equipmentTypeIcon;
    [SerializeField] private Image _equipmentUpgradeValueIcon;
    [SerializeField] private Text _equipmentUpgradeValueName;
    [SerializeField] private Text _equipmentUpgradeValue;
    [SerializeField] private Text _equipmentLevel;

    [Header("Quality skills menu")]
    [SerializeField] private Sprite _onLockedIcon;

    [Space(5)]
    [SerializeField] private Image _commonCircleImage;
    [SerializeField] private Image _commonLockedImage;
    [SerializeField] private Text _commonDescription;

    [Space(5)]
    [SerializeField] private Image _rareCircleImage;
    [SerializeField] private Image _rareLockedImage;
    [SerializeField] private Text _rareDescription;

    [Space(5)]
    [SerializeField] private Image _epicCircleImage;
    [SerializeField] private Image _epicLockedImage;
    [SerializeField] private Text _epicDescription;

    [Space(5)]
    [SerializeField] private Image _legendaryCircleImage;
    [SerializeField] private Image _legendaryLockedImage;
    [SerializeField] private Text _legendaryDescription;

    [Space(5)]
    [SerializeField] private Image _SSRCircleImage;
    [SerializeField] private Image _SSRLockedImage;
    [SerializeField] private Text _SSRDescription;

    [Header("Upgrade settings")]
    [SerializeField] private Image _materialIcon;
    [SerializeField] private Text _materialCountText;

    [SerializeField] private Image _currencyIcon;
    [SerializeField] private Text _currencyCountText;

    [Space(5)]
    [SerializeField] private Text _maxLevelTootlipText;
    [SerializeField] private Text _equipUnequipButtonText;

    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Button _quickUpgradeButton;

    private EquipmentTypesData _equipmentTypesData;
    private Equipment _equipment;

    public EquipmentTypesData EquipmentTypesData => _equipmentTypesData;

    public override void Initialize(MainMenu mainMenu, UIMenu parentMenu = null)
    {
        base.Initialize(mainMenu, parentMenu);

        if (_parentMenu != null)
        {
            _equipmentTypesData = (_parentMenu as InventoryMenu).EquipmentTypesData;
        }
        else if (_isDebug) Debug.Log("Missing InventoryMenu!");

        _resetMenu.Initialize(mainMenu, this);
    }

    public override void Display(bool playAnimation = false)
    {
        _resetMenu.Hide();

        base.Display(playAnimation);
    }

    public override void Hide(bool playAnimation = false)
    {
        _resetMenu.Hide();

        base.Hide(playAnimation);
    }

    public void SetEquipment(Equipment equipment)
    {
        if (equipment == null) return;

        _equipment = equipment;

        if (_equipment.isEquiped) _equipUnequipButtonText.text = "Unequip";
        else _equipUnequipButtonText.text = "Equip";

        _rarityText.text = _equipment.EquipRarity.ToString();
        _rarityBackground.sprite = _equipmentTypesData[_equipment.EquipRarity].RarityBackground;
        _equipmentIcon.sprite = _equipment.Icon;
        _equipmentTypeIcon.sprite = _equipmentTypesData[_equipment.EquipSlot].SlotIcon;

        _equipmentUpgradeValueIcon.sprite = _equipment.UpgradingStat.Equals(UpgradingStat.Health) ? _healthIcon : _damageIcon;

        float value = 0;

        foreach(UpgradeData data in _equipment.EquipUpgrade.Upgrades)
        {
            value += data.UpgradeValue;
        }

        _equipmentUpgradeValueName.text = _equipment.UpgradingStat.Equals(UpgradingStat.Health) ? "Health" : "Damage";
        _equipmentUpgradeValue.text = ((int)value).ToString();

        _equipmentLevel.text = ((int)_equipment.Level.Value).ToString() + "/" + ((int)_equipment.Level.MaxValue).ToString();

        if (_equipment.Level.Value == _equipment.Level.MaxValue)
        {
            _upgradeButton.interactable = false;
            _quickUpgradeButton.interactable = false;

            _materialIcon.enabled = false;
            _materialCountText.enabled = false;
            _currencyIcon.enabled = false;
            _currencyCountText.enabled = false;

            _maxLevelTootlipText.enabled = true;
        }
        else
        {
            _upgradeButton.interactable = true;
            _quickUpgradeButton.interactable = true;

            _materialIcon.enabled = true;
            _materialCountText.enabled = true;
            _currencyIcon.enabled = true;
            _currencyCountText.enabled = true;

            _maxLevelTootlipText.enabled = false;
        }
    }

    public void OnResetClick()
    {
        base.Hide(true);

        _resetMenu.SetEquipment(_equipment);
        _resetMenu.Display(true);
    }

    public void OnEquipUnequipClick()
    {
        if (_equipment == null) return;

        if (_equipment.isEquiped)
        {
            (_parentMenu as InventoryMenu).Unequip(_equipment);
        }
        else
        {
            (_parentMenu as InventoryMenu).Equip(_equipment);
        }
    }

    public void OnUpgradeClick()
    {

    }

    public void OnQuickUpgradeClick()
    {

    }

    public void OnCloseMenu()
    {
        Hide(true);
    }
}
