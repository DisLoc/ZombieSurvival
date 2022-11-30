using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class ItemResetMenu : UIMenu
{
    [Header("Item reset menu settings")]
    [SerializeField] private Text _mainButtonText;
    [SerializeField] private Button _mainButton;

    [Space(5)]
    [SerializeField] private GameObject _arrow;
    [SerializeField] private GameObject _returnedMaterials;
    [SerializeField] private Text _cantDowngradeTooltipText;   
    [SerializeField] private Text _cantResetTooltipText;   

    [Space(5)]
    [SerializeField] private EquipmentSlot _currentEquipmentSlot;

    [Space(5)]
    [SerializeField] private Image _resultEquipmentBackground;
    [SerializeField] private Image _resultEquipmentTypeIcon;
    [SerializeField] private Image _resultEquipmentIcon;
    [SerializeField] private Text _resultEquipmentCountText;
    private int _returnedEquipmentAmount;

    [Space(5)]
    [SerializeField] private Sprite _defaultCurrencyBackground;
    [SerializeField] private Image _currencyBackground;
    [SerializeField] private Image _resultCurrencyIcon;
    [SerializeField] private Text _resultCurrencyCountText;
    private int _returnedCurrencyAmount;

    [Space(5)]
    [SerializeField] private Sprite _defaultMaterialBackground;
    [SerializeField] private Image _materialBackground;
    [SerializeField] private Image _resultMaterialIcon;
    [SerializeField] private Text _resultMaterialCountText;
    private int _returnedMaterialsAmount;

    private Equipment _equipment;
    private EquipmentTypesData _typesData;
    private MainButtonStates _currentButtonState;

    public override void Initialize(MainMenu mainMenu, UIMenu parentMenu = null)
    {
        base.Initialize(mainMenu, parentMenu);
        
        if (_parentMenu != null)
        {
            _typesData = (_parentMenu as ItemUpgradeMenu).EquipmentTypesData;
            _currentEquipmentSlot.Initialize(_typesData);
        }
        else if (_isDebug) Debug.Log("Missing InventoryMenu!");

        _currencyBackground.sprite = _defaultCurrencyBackground;
        _materialBackground.sprite = _defaultMaterialBackground;

        _currentButtonState = MainButtonStates.LevelReset;
        SetButtonText();
    }

    public void SetEquipment(Equipment equipment)
    {
        _equipment = equipment;
        _currentEquipmentSlot.SetSlot(equipment);

        _currentButtonState = MainButtonStates.LevelReset;
        SetButtonText();

        _returnedMaterialsAmount = 0;
        _returnedCurrencyAmount = 0;
        _returnedEquipmentAmount = 0;
    }

    public void OnCloseMenu()
    {
        (_parentMenu as ItemUpgradeMenu).Display();

        Hide(true);
    }

    public void OnMainButtonClick()
    {
        if (_isDebug) Debug.Log("MainButtonClick");

        if (_currentButtonState.Equals(MainButtonStates.LevelReset))
        {
            ResetItemLevel();
        }
        else
        {
            DowngradeItemQuality();
        }
    }

    public void OnResetLevelsClick()
    {
        _currentButtonState = MainButtonStates.LevelReset;
        SetButtonText();
    }

    public void OnQualityDowngradeClick()
    {
        _currentButtonState = MainButtonStates.QualityDowngrade;
        SetButtonText();
    }

    private void ResetItemLevel()
    {
        if (_isDebug) Debug.Log("Reseting item level...");

        _equipment.Level.SetValue(1);

        SetEquipment(_equipment);
        (_parentMenu as ItemUpgradeMenu).UpdateInventory();
    }

    private void DowngradeItemQuality()
    {
        if (_isDebug) Debug.Log("Downgrade item quality...");

        SetEquipment(_equipment);
        (_parentMenu as ItemUpgradeMenu).UpdateInventory();
    }

    private void SetButtonText()
    {
        _mainButtonText.text = _currentButtonState.Equals(MainButtonStates.LevelReset) ? "Level reset" : "Quality downgrade"; 
        UpdateResetingData();
    }

    private void UpdateResetingData()
    {
        if (_equipment == null) return;

        List<EquipmentUpgrade> upgrades = _equipment.EquipmentData.EquipmentUpgrades.Upgrades.FindAll(item => item.RequiredLevel <= _equipment.Level.Value);

        #region Calculate total materials
        int totalCurrency = 0, totalMaterials = 0;

        foreach (var upgrade in upgrades)
        {
            totalCurrency += upgrade.UpgradeMaterials.RequiredCurrencyAmount;
            totalMaterials += upgrade.UpgradeMaterials.RequiredMaterialAmount;
        }

        _returnedCurrencyAmount = totalCurrency;
        _returnedMaterialsAmount = totalMaterials;

        _currencyBackground.sprite = _equipment.EquipmentData.EquipmentUpgrades.RequiredCurrency.Background;
        _resultCurrencyIcon.sprite = _equipment.EquipmentData.EquipmentUpgrades.RequiredCurrency.Icon;
        _resultCurrencyCountText.text = "x" + totalCurrency.ToString();

        _materialBackground.sprite = _equipment.EquipmentData.EquipmentUpgrades.RequiredMaterial.Background;
        _resultMaterialIcon.sprite = _equipment.EquipmentData.EquipmentUpgrades.RequiredMaterial.Icon;
        _resultMaterialCountText.text = "x" + totalMaterials.ToString();
        #endregion

        #region State.LevelReset
        if (_currentButtonState.Equals(MainButtonStates.LevelReset))
        {
            if ((int)_equipment.Level.Value == 1)
            {
                _arrow.SetActive(false);
                _returnedMaterials.SetActive(false);

                _cantResetTooltipText.enabled = true;
                _cantDowngradeTooltipText.enabled = false;

                _mainButton.interactable = false;

                return;
            }

            _arrow.SetActive(true);
            _returnedMaterials.SetActive(true);

            _cantResetTooltipText.enabled = false;
            _cantDowngradeTooltipText.enabled = false;

            _mainButton.interactable = true;

            _resultEquipmentBackground.enabled = false;
            _resultEquipmentTypeIcon.enabled = false;
            _resultEquipmentIcon.enabled = false;
            _resultEquipmentCountText.enabled = false;
        }
        #endregion
        #region State.QualityDowngrade
        else
        {
            if (_equipment.EquipRarity.Equals(EquipRarity.Common))
            {
                _arrow.SetActive(false);
                _returnedMaterials.SetActive(false);

                _cantDowngradeTooltipText.enabled = true;
                _cantResetTooltipText.enabled = false;

                _mainButton.interactable = false;

                return;
            }

            _arrow.SetActive(true);
            _returnedMaterials.SetActive(true);

            _cantResetTooltipText.enabled = false;
            _cantDowngradeTooltipText.enabled = false;

            _mainButton.interactable = true;

            _resultEquipmentBackground.enabled = true;
            _resultEquipmentTypeIcon.enabled = true;
            _resultEquipmentIcon.enabled = true;
            _resultEquipmentCountText.enabled = true;

            int totalEquipment = 0;

            foreach (var upgrade in upgrades)
            {
                totalEquipment += upgrade.UpgradeMaterials.RequiredEquipmentAmount;
            }

            _resultEquipmentBackground.sprite = _typesData[_equipment.EquipmentData.EquipmentUpgrades.RequiredEquipment.EquipRarity].RarityBackground;
            _resultEquipmentTypeIcon.sprite = _typesData[_equipment.EquipSlot].SlotIcon;
            _resultEquipmentIcon.sprite = _equipment.EquipmentData.EquipmentUpgrades.RequiredEquipment.Icon;
            _resultEquipmentCountText.text = "x" + totalEquipment.ToString();
        }
        #endregion
    }

    private enum MainButtonStates
    {
        LevelReset,
        QualityDowngrade
    }
}
