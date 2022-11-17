using UnityEngine;
using UnityEngine.UI;

public sealed class ItemResetMenu : UIMenu
{
    [Header("Item reset menu settings")]
    [SerializeField] private Text _mainButtonText;

    [Space(5)]
    [SerializeField] private EquipmentSlot _currentEquipmentIcon;

    [Space(5)]
    [SerializeField] private Image _resultEquipmentBackground;
    [SerializeField] private Image _resultEquipmentTypeIcon;
    [SerializeField] private Image _resultEquipmentIcon;
    [SerializeField] private Text _resultEquipmentCountText;

    [Space(5)]
    [SerializeField] private Sprite _defaultCurrencyBackground;
    [SerializeField] private Image _currencyBackground;
    [SerializeField] private Image _resultCurrencyIcon;
    [SerializeField] private Text _resultCurrencyCountText;

    [Space(5)]
    [SerializeField] private Sprite _defaultMaterialBackground;
    [SerializeField] private Image _materialBackground;
    [SerializeField] private Image _resultMaterialIcon;
    [SerializeField] private Text _resultMaterialCountText;

    private Equipment _equipment;
    private MainButtonStates _currentButtonState;

    public override void Initialize(MainMenu mainMenu, UIMenu parentMenu = null)
    {
        base.Initialize(mainMenu, parentMenu);

        _currentButtonState = MainButtonStates.LevelReset;
        SetButtonText();
    }

    public void SetEquipment(Equipment equipment)
    {
        _equipment = equipment;
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
       
    }

    private void DowngradeItemQuality()
    {
        if (_isDebug) Debug.Log("Downgrade item quality...");
    }

    private void SetButtonText()
    {
        _mainButtonText.text = _currentButtonState.Equals(MainButtonStates.LevelReset) ? "Level reset" : "Quality downgrade"; 
        UpdateResetingData();
    }

    private void UpdateResetingData()
    {
        if (_currentButtonState.Equals(MainButtonStates.LevelReset))
        {

        }
        else
        {

        }
    }

    private enum MainButtonStates
    {
        LevelReset,
        QualityDowngrade
    }
}
