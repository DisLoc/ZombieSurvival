using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CampMenu : UIMenu
{
    [Header("Camp menu settings")]
    [SerializeField] private CampTalentsMenu _talents;
    [SerializeField] private CampUpgradesMenu _upgrades;
    [SerializeField] private List<UpgradableBuilding> _buildings;

    [Space(5)]
    [SerializeField] private Text _campLevelText;

    [Space(5)]
    [SerializeField] private GameObject _cantUpgradeTooltip;
    [SerializeField] private Text _tooltipText;

    [Space(5)]
    [SerializeField] private GameObject _upgradeButton;
    [SerializeField] private Image _requiredCurrencyIconImage;
    [SerializeField] private Text _requiredCurrencyAmountText;

    [Header("Upgrades settings")]
    [SerializeField] private List<CampUpgradeLimit> _campLevelsLimit;
    [SerializeField] private Currency _requiredCurrency;
    [SerializeField] private float _currencyMultiplierPerLevel;

    private Currency _currentCost;

    [Inject] private MainInventory _mainInventory;

    public override void Initialize(MainMenu mainMenu, UIMenu parentMenu = null)
    {
        base.Initialize(mainMenu, parentMenu);

        _talents.Initialize(_mainMenu, this);
        _upgrades.Initialize(_mainMenu, this);
    }

    public override void Display(bool playAnimation = false)
    {
        _talents.Hide();
        _upgrades.Hide();

        foreach(var building in _buildings)
        {
            building.CheckLocked();
        }

        UpdateUpgrades();

        base.Display(playAnimation);
    }

    public override void Hide(bool playAnimation = false)
    {
        _talents.Hide();
        _upgrades.Hide();

        base.Hide(playAnimation);
    }

    public void OnUpgradeClick()
    {
        if (_mainInventory.EnoughResources(_currentCost))
        {
            List<UpgradableBuilding> buildings = _buildings.FindAll(item => item.Unlocked && item.CampUpgrade.Level.Value < item.CampUpgrade.Level.MaxValue);

            if (buildings.Count == 0)
            {
                _mainMenu.ShowPopupMessage("Nothing to upgrade!");
                return;
            }

            if (_mainInventory.Spend(_currentCost))
            {
                buildings[Random.Range(0, buildings.Count)].CampUpgrade.Upgrade();

                _mainInventory.SaveInventory(_mainInventory.CampInventory);

                UpdateUpgrades();
            }
        }
        else
        {
            _mainMenu.ShowPopupMessage("Not enough resources!");
        }
    }

    private void UpdateUpgrades()
    {
        int currentLimitIndex = _campLevelsLimit.FindLastIndex(item => item.RequiredPlayerLevel <= _mainInventory.PlayerLevel.Value);

        int currentLevel = 0;

        for (int i = 0; i < _mainInventory.CampInventory.Upgrades.Count; i++)
        {
            currentLevel += (int)_mainInventory.CampInventory.Upgrades[i].Level.Value;
            _mainInventory.CampInventory.Upgrades[i].UpdateValues();
        }

        _campLevelText.text = "Camp lvl " + currentLevel;

        if (currentLimitIndex == _campLevelsLimit.Count - 1)
        {
            _upgradeButton.SetActive(false);
            _cantUpgradeTooltip.SetActive(true);

            _tooltipText.text = "Reached max camp level for now!";
        }
        else if (currentLimitIndex == -1)
        {
            _upgradeButton.SetActive(false);
            _cantUpgradeTooltip.SetActive(true);

            _tooltipText.text = "Requires lvl " + _campLevelsLimit[0].RequiredPlayerLevel + " to start upgrading camp";
        }
        else
        {
            if (currentLevel >= _campLevelsLimit[currentLimitIndex].MaxCampLevel)
            {
                _upgradeButton.SetActive(false);
                _cantUpgradeTooltip.SetActive(true);

                _tooltipText.text = "Requires lvl " + _campLevelsLimit[currentLimitIndex + 1].RequiredPlayerLevel + " to continue upgrading camp";
            }
            else
            {
                _upgradeButton.SetActive(true);
                _cantUpgradeTooltip.SetActive(false);

                _currentCost = new Currency
                    (
                        _requiredCurrency.CurrencyData, 
                        _requiredCurrency.CurrencyValue * (currentLevel > 0 ? (int)(currentLevel * _currencyMultiplierPerLevel) : 1)
                    );

                _requiredCurrencyAmountText.text = _currentCost.CurrencyValue.ToString();
                _requiredCurrencyIconImage.sprite = _currentCost.CurrencyData.Icon;
            }
        }
    }
}
