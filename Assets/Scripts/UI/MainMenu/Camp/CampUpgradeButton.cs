using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CampUpgradeButton : MonoBehaviour
{
    [SerializeField] private UpgradableBuilding _requiredBuilding;
    [SerializeField] private Button _button;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Image _lockedImage;

    [SerializeField] private Text _levelText;
    [SerializeField] private Text _valueText;

    private CampUpgrade _upgrade;

    [Inject] private MainMenu _mainMenu;
    [Inject] private MainInventory _mainInventory;

    public void Initialize(CampUpgrade upgrade)
    {
        _upgrade = upgrade;

        UpdateValues();
    }

    public void UpdateValues()
    {
        if (_upgrade == null) return;

        _iconImage.sprite = _upgrade.Icon;

        if (_upgrade.Level.Value == _upgrade.Level.MaxValue)
        {
            _button.interactable = false;
            _levelText.text = "MAX";
        }
        else
        {
            _button.interactable = true;
            _levelText.text = _upgrade.Level.Value == 0 ? "Unlock" : ("Level " + (int)_upgrade.Level.Value);
        }

        if (_upgrade.Level.Value == 0)
        {
            _lockedImage.enabled = true;
            _iconImage.enabled = false;
            _valueText.enabled = false;
        }
        else
        {
            _lockedImage.enabled = false;
            _iconImage.enabled = true; 
            _valueText.enabled = true; 
            
            int value = 0;
            foreach (var data in _upgrade.CurrentUpgrade.Upgrades)
            {
                value += (int)data.UpgradeValue;
            }

            _valueText.text = (value > 0 ? "+ " : "") + value;
        }
    }

    public void OnClick()
    {
        if (_requiredBuilding.Unlocked)
        {
            if (_mainInventory.Spend(_upgrade.CurrentUpgradeCost))
            {
                _mainInventory.CampInventory.Upgrade(_upgrade);
            }
            else
            {
                _mainMenu.ShowPopupMessage("Not enough resources!");
            }
        }
        else
        {
                _mainMenu.ShowPopupMessage("This upgrade will be unlocked at level " + _requiredBuilding.UnlockLevel);
        }
    }
}

