using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ItemUpgradeMenu : UIMenu
{
    [Header("Upgrade menu settings")]
    [SerializeField] private Image _hideButton;
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
    [SerializeField] private Image _uncommonCircleImage;
    [SerializeField] private Image _uncommonLockedImage;
    [SerializeField] private Text _uncommonDescription;
    
    [Space(5)]
    [SerializeField] private Image _rareCircleImage;
    [SerializeField] private Image _rareLockedImage;
    [SerializeField] private Text _rareDescription;

    [Space(5)]
    [SerializeField] private Image _excellentCircleImage;
    [SerializeField] private Image _excellentLockedImage;
    [SerializeField] private Text _excellentDescription;

    [Space(5)]
    [SerializeField] private Image _epicCircleImage;
    [SerializeField] private Image _epicLockedImage;
    [SerializeField] private Text _epicDescription;

    [Space(5)]
    [SerializeField] private Image _superiorCircleImage;
    [SerializeField] private Image _superiorLockedImage;
    [SerializeField] private Text _superiorDescription;

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

    [Inject] private MainInventory _mainInventory;

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
        _hideButton.raycastTarget = true;

        if (_equipment != null)
        {
            _resetMenu.Hide();

            base.Display(playAnimation);
        }
        else Hide();
    }

    public override void Hide(bool playAnimation = false)
    {
        _hideButton.raycastTarget = false;
        _resetMenu.Hide();

        base.Hide(playAnimation);
    }

    public void SetEquipment(Equipment equipment)
    {
        if (equipment == null) return;

        _equipment = equipment;

        #region Initialization
        if (_equipment.isEquiped) _equipUnequipButtonText.text = "Unequip";
        else _equipUnequipButtonText.text = "Equip";

        _rarityText.text = _equipment.EquipRarity.ToString();
        _rarityBackground.sprite = _equipmentTypesData[_equipment.EquipRarity].RarityBackground;
        _equipmentIcon.sprite = _equipment.Icon;
        _equipmentTypeIcon.sprite = _equipmentTypesData[_equipment.EquipSlot].SlotIcon;

        _equipmentUpgradeValueIcon.sprite = _equipment.UpgradingStat.Equals(UpgradingStat.Health) ? _healthIcon : _damageIcon;
        #endregion

        #region Equipment values
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

            _materialIcon.sprite = equipment.EquipmentData.EquipmentUpgrades.RequiredMaterial.Icon;
            _materialCountText.text = _mainInventory.MaterialsInventory[equipment.EquipSlot].ToString() +
                "/" + equipment.CurrentUpgrade.UpgradeMaterials.RequiredMaterialAmount.ToString();

            _currencyIcon.sprite = equipment.EquipmentData.EquipmentUpgrades.RequiredCurrency.Icon;
            _currencyCountText.text = _mainInventory.FindInventory(equipment.EquipmentData.EquipmentUpgrades.RequiredCurrency).Total.ToString() +
                "/" + equipment.CurrentUpgrade.UpgradeMaterials.RequiredCurrencyAmount.ToString();
        }
        #endregion

        #region Rarity upgrades
        Dictionary<EquipRarity, string> equipmentRarities = new Dictionary<EquipRarity, string>();

        EquipmentData currentData = equipment.EquipmentData;

        equipmentRarities.Add(currentData.EquipRarity, currentData.RarityDescription);

        while (currentData.PreviousRarityEquipment != null)
        {
            currentData = currentData.PreviousRarityEquipment;
            
            if (equipmentRarities.ContainsKey(currentData.EquipRarity))
            {
                if (_isDebug) Debug.Log("Rarity error! Repeating rarity in " + equipment);

                break;
            }
            else
            {
                equipmentRarities.Add(currentData.EquipRarity, currentData.RarityDescription);
            }
        }

        currentData = equipment.EquipmentData;

        while (currentData.NextRarityEquipment != null)
        {
            currentData = currentData.NextRarityEquipment;

            if (equipmentRarities.ContainsKey(currentData.EquipRarity))
            {
                if (_isDebug) Debug.Log("Rarity error! Repeating rarity in " + equipment);

                break;
            }
            else
            {
                equipmentRarities.Add(currentData.EquipRarity, currentData.RarityDescription);
            }
        }

        foreach(var rarity in equipmentRarities)
        {
            switch (rarity.Key)
            {
                case EquipRarity.Common:
                    _commonCircleImage.sprite = _equipmentTypesData[rarity.Key].RarityCircle;
                    _commonLockedImage.enabled = equipment.EquipRarity < rarity.Key;
                    _commonLockedImage.sprite = _onLockedIcon;
                    _commonDescription.text = rarity.Value;
                    break;

                case EquipRarity.Uncommon:
                    _uncommonCircleImage.sprite = _equipmentTypesData[rarity.Key].RarityCircle;
                    _uncommonLockedImage.enabled = equipment.EquipRarity < rarity.Key;
                    _uncommonLockedImage.sprite = _onLockedIcon;
                    _uncommonDescription.text = rarity.Value;
                    break;

                case EquipRarity.Rare:
                    _rareCircleImage.sprite = _equipmentTypesData[rarity.Key].RarityCircle;
                    _rareLockedImage.enabled = equipment.EquipRarity < rarity.Key;
                    _rareLockedImage.sprite = _onLockedIcon;
                    _rareDescription.text = rarity.Value;
                    break;

                case EquipRarity.Excellent:
                    _excellentCircleImage.sprite = _equipmentTypesData[rarity.Key].RarityCircle;
                    _excellentLockedImage.enabled = equipment.EquipRarity < rarity.Key;
                    _excellentLockedImage.sprite = _onLockedIcon;
                    _excellentDescription.text = rarity.Value;
                    break;

                case EquipRarity.Epic:
                    _epicCircleImage.sprite = _equipmentTypesData[rarity.Key].RarityCircle;
                    _epicLockedImage.enabled = equipment.EquipRarity < rarity.Key;
                    _epicLockedImage.sprite = _onLockedIcon;
                    _epicDescription.text = rarity.Value;
                    break;

                case EquipRarity.Superior:
                    _superiorCircleImage.sprite = _equipmentTypesData[rarity.Key].RarityCircle;
                    _superiorLockedImage.enabled = equipment.EquipRarity < rarity.Key;
                    _superiorLockedImage.sprite = _onLockedIcon;
                    _superiorDescription.text = rarity.Value;
                    break;

                default:
                    if (_isDebug) Debug.Log("Missing rarity!");
                    break;
            }
        }
        #endregion
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
            _equipUnequipButtonText.text = "Equip";
        }
        else
        {
            (_parentMenu as InventoryMenu).Equip(_equipment);
            _equipUnequipButtonText.text = "Unequip";
        }
    }

    public void OnUpgradeClick()
    {
        if (_equipment != null)
        {
            if (_equipment.Level.Value != _equipment.Level.MaxValue && 
                _mainInventory.EnoughResources(_equipment.CurrentUpgrade.UpgradeMaterials, _equipment.EquipmentData))
            {
                _mainInventory.Spend(_equipment.EquipmentData.EquipmentUpgrades.RequiredMaterial, 
                    _equipment.CurrentUpgrade.UpgradeMaterials.RequiredMaterialAmount);

                _mainInventory.Spend(new Currency(_equipment.EquipmentData.EquipmentUpgrades.RequiredCurrency,
                    _equipment.CurrentUpgrade.UpgradeMaterials.RequiredCurrencyAmount));
                
                _mainInventory.Spend(_equipment.EquipmentData.EquipmentUpgrades.RequiredEquipment,
                    _equipment.CurrentUpgrade.UpgradeMaterials.RequiredEquipmentAmount);

                _equipment.Level.LevelUp();

                SetEquipment(_equipment);

                UpdateInventory();
            }
            else
            {
                _mainMenu.ShowPopupMessage("Not enough resources!");
            }
        }
    }

    public void OnQuickUpgradeClick()
    {
        if (_equipment != null)
        {
            if (_mainInventory.EnoughResources(_equipment.CurrentUpgrade.UpgradeMaterials, _equipment.EquipmentData))
            {
                while (_equipment.Level.Value != _equipment.Level.MaxValue &&
                                _mainInventory.EnoughResources(_equipment.CurrentUpgrade.UpgradeMaterials, _equipment.EquipmentData))
                {
                    OnUpgradeClick();
                }
            }
            else
            {
                _mainMenu.ShowPopupMessage("Not enough resources!");
            }
        }
    }

    public void OnEquipmentDowngrade(Equipment equipment)
    {
        _equipment = null;

        (_parentMenu as InventoryMenu).RemoveEquipment(equipment);

        _mainInventory.Spend(equipment);

        StartCoroutine(WaitDisplay());
    }

    private IEnumerator WaitDisplay()
    {
        yield return new WaitForEndOfFrame();

        _parentMenu.Display();
    }

    public void UpdateInventory()
    {
        (_parentMenu as InventoryMenu).UpdateValues();

        if (_equipment != null)
        {
            SetEquipment(_equipment);
        }
    }

    public void OnCloseMenu()
    {
        Hide(true);
    }
}