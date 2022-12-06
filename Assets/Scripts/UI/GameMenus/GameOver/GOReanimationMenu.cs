using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GOReanimationMenu : UIMenu
{
    [Header("Reanimation menu settings")]
    [SerializeField] private Text _timerText; 
    [SerializeField][Range(5, 20)] private int _reanimationMaxTimer = 10;

    [Space(5)]
    [SerializeField] private Currency _reanimationCost;

    [Header("Ad reanimation")]
    [Tooltip("Must have only 1 UpgradeData for correct work")]
    [SerializeField] private Upgrade _reanimationByAdHealUpgrade;
    [SerializeField] private Sprite _reanimationByAdHeartSprite;
    [SerializeField] private Image _reanimationByAdHeartImage;
    [SerializeField] private Text _reanimationByAdHealthText;

    [Header("Currency reanimation")]
    [Tooltip("Must have only 1 UpgradeData for correct work")]
    [SerializeField] private Upgrade _reanimationByCurrencyHealUpgrade;
    [SerializeField] private Sprite _reanimationByCurrencyHeartSprite;
    [SerializeField] private Image _reanimationByCurrencyHeartImage;
    [SerializeField] private Text _reanimationByCurrencyHealthText;
    [SerializeField] private Text _reanimationByCurrencyCostText;
    [SerializeField] private Image _reanimationByCurrencyIcon;

    private int _timer;

    public Currency ReanimationCost => _reanimationCost;
    public Upgrade ReanimationByAdHealUpgrade => _reanimationByAdHealUpgrade;
    public Upgrade ReanimationByCurrencyHealUpgrade => _reanimationByCurrencyHealUpgrade;

    public override void Initialize(MainMenu mainMenu, UIMenu parentMenu = null)
    {
        base.Initialize(mainMenu, parentMenu);

        _reanimationByAdHealthText.text = (_reanimationByAdHealUpgrade.Upgrades[0].UpgradeMultiplier * 100).ToString() + "%";
        _reanimationByAdHeartImage.sprite = _reanimationByAdHeartSprite;


        _reanimationByCurrencyHealthText.text = (_reanimationByCurrencyHealUpgrade.Upgrades[0].UpgradeMultiplier * 100).ToString() + "%";
        _reanimationByCurrencyHeartImage.sprite = _reanimationByCurrencyHeartSprite;
        _reanimationByCurrencyIcon.sprite = _reanimationCost.CurrencyData.Icon;
        _reanimationByCurrencyCostText.text = _reanimationCost.CurrencyValue.ToString();
    }

    public override void Display(bool playAnimation = false)
    {
        base.Display(playAnimation);

        _timer = _reanimationMaxTimer;
        StartCoroutine(UpdateTimer());
    }

    public override void Hide(bool playAnimation = false)
    {
        base.Hide(playAnimation);

        _timer = _reanimationMaxTimer;
        StopAllCoroutines();
    }

    private IEnumerator UpdateTimer()
    {
        _timerText.text = _timer.ToString();

        if (_timer <= 0)
        {
            (_parentMenu as GameOverMenu).OnCloseReanimation();
            yield return null;
        }
        else
        {
            yield return new WaitForSecondsRealtime(1);

            _timer--;
            StartCoroutine(UpdateTimer());
        }
    }
}
