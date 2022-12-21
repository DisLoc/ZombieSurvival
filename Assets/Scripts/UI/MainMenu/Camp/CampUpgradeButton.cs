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

    public void Initialize(CampUpgrade upgrade)
    {
        _upgrade = upgrade;

        _requiredBuilding.Initialize(upgrade);

        UpdateValues();
    }

    public void UpdateValues()
    {
        if (_upgrade == null) return;

        _iconImage.sprite = _upgrade.Icon;

        if (_upgrade.Level.Value == _upgrade.Level.MaxValue)
        {
            _levelText.text = "MAX";
        }
        else
        {
            _levelText.text = "Level " + (int)_upgrade.Level.Value;
        }

        if (!_requiredBuilding.Unlocked)
        {
            _button.interactable = true;
            _lockedImage.enabled = true;
            _iconImage.enabled = false;
            _valueText.enabled = false;
            _levelText.enabled = false;
        }
        else
        {
            _button.interactable = false;
            _lockedImage.enabled = false;
            _iconImage.enabled = true; 
            _valueText.enabled = true;
            _levelText.enabled = true;

            int value = 0;
            if (_upgrade.Level.Value > 0)
            {
                foreach (var data in _upgrade.CurrentUpgrade.Upgrades)
                {
                    value += (int)data.UpgradeValue;
                }
            }

            _valueText.text = (value > 0 ? "+ " : "") + value;
        }
    }

    public void OnClick()
    {
        if (!_requiredBuilding.Unlocked)
        {
            _mainMenu.ShowPopupMessage("This upgrade will be unlocked at level " + _requiredBuilding.UnlockLevel);
        }
    }
}

