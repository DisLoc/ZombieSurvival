using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public sealed class GameOverMenu : UIMenu, IPlayerDieHandler
{
    #region GameOver
    [Header("Game over menu settings")]
    [SerializeField] private UIMenu _gameOverMenu;

    [SerializeField] private ZombieCounter _enemyCounter;
    [SerializeField] private SurvivalTimeCounter _survivalTimeCounter;

    [SerializeField] private Text _totalKilledText;
    [SerializeField] private Text _survivalTimeText;

    public void OnContinue()
    {
        SceneManager.LoadScene(0);
    }
    #endregion

    #region Reanimatoin
    [Header("Reanimation menu settings")]
    [SerializeField] private UIMenu _reanimationMenu;

    [Space(5)]
    [SerializeField] private Text _timerText;

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

    [Space(5)]
    [SerializeField] private CurrencyData _reanimationCurrencyData;
    [SerializeField][Range(1, 1000)] private int _reanimationByCurrencyCost;

    private int _reanimations;

    [Inject] private Player _player;

    public void OnCurrencyReanimation()
    {
        _reanimations++;
        _player.GetUpgrade(_reanimationByCurrencyHealUpgrade);

        _mainMenu.DisplayDefault();
    }

    public void OnAdReanimation()
    {
        return;

        /*
        _player.GetUpgrade(_reanimationByAdHealUpgrade);
        _reanimations++;

        _mainMenu.DisplayDefault();
         */
    }

    public void OnClose()
    {
        SceneManager.LoadScene(0);
    }
    #endregion

    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    public override void Initialize(MainMenu mainMenu, UIMenu parentMenu = null)
    {
        base.Initialize(mainMenu, parentMenu);

        _reanimations = 0;

        _reanimationByAdHealthText.text = (_reanimationByAdHealUpgrade.Upgrades[0].UpgradeMultiplier * 100).ToString() + "%";
        _reanimationByAdHeartImage.sprite = _reanimationByAdHeartSprite;


        _reanimationByCurrencyHealthText.text = (_reanimationByCurrencyHealUpgrade.Upgrades[0].UpgradeMultiplier * 100).ToString() + "%";
        _reanimationByCurrencyHeartImage.sprite = _reanimationByCurrencyHeartSprite;
        _reanimationByCurrencyIcon.sprite = _reanimationCurrencyData.Icon;
        _reanimationByCurrencyCostText.text = _reanimationByCurrencyCost.ToString();
    }

    public override void Hide(bool playAnimation = false)
    {
        base.Hide(playAnimation);

        _reanimationMenu.Hide(playAnimation);
        _gameOverMenu.Hide(playAnimation);
    }

    public override void Display(bool playAnimation = false)
    {
        base.Display(playAnimation);

        _reanimationMenu.Hide();
        _gameOverMenu.Hide();
    }

    public void OnPlayerDie()
    {

        _mainMenu.Display(this);

        if (_reanimations < (_player.Stats as PlayerStats).RebornCount.Value)
        {
            _gameOverMenu.Hide();
            _reanimationMenu.Display(true);
        }
        else
        {
            _reanimationMenu.Hide();
            _gameOverMenu.Display(true);
        }
    }
}
