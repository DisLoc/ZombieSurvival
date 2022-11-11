using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public sealed class GameOverMenu : UIMenu, IPlayerDieHandler
{
    #region GameOver
    [Header("Game over menu settings")]
    [SerializeField] private GOGameOverMenu _gameOverMenu;

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
    [SerializeField] private GOReanimationMenu _reanimationMenu;

    [SerializeField] private Text _timerText;

    [SerializeField] private Text _reanimationByAdHealthText;

    [SerializeField] private Text _reanimationByCurrencyHealthText;
    [SerializeField] private Text _reanimationByCurrencyCostText;
    [SerializeField] private Image _reanimationByCurrencyIcon;

    private int _reanimations;

    [Inject] private Player _player;

    public void OnCurrencyReanimation()
    {
        _reanimations++;

        Hide();
    }

    public void OnAdReanimation()
    {
        _reanimations++;

        Hide();
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

    public override void Initialize(MainMenu mainMenu)
    {
        base.Initialize(mainMenu);

        _reanimations = 0;
    }

    public override void Hide(bool playAnimation = false)
    {
        base.Hide(playAnimation);

        _reanimationMenu.Hide(playAnimation);
        _gameOverMenu.Hide(playAnimation);

        Time.timeScale = 1;
    }

    public override void Display(bool playAnimation = false)
    {
        base.Display(playAnimation);

        _reanimationMenu.Hide();
        _gameOverMenu.Hide();

        Time.timeScale = 0;
    }

    public void OnPlayerDie()
    {
        Time.timeScale = 0;

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
