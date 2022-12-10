using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public sealed class GameOverMenu : UIMenu, IPlayerDieHandler
{
    [Header("Game over menu settings")]
    [SerializeField] private GOGameOverMenu _gameOverMenu;
    [SerializeField] private GOReanimationMenu _reanimationMenu;

    private int _reanimations;

    [Inject] private Player _player;
    [Inject] private MainInventory _mainInentory;

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

        _gameOverMenu.Initialize(mainMenu, this);
        _reanimationMenu.Initialize(mainMenu, this);

        _reanimations = 0;
    }

    public override void Display(bool playAnimation = false)
    {
        base.Display(playAnimation);

        _reanimationMenu.Hide();
        _gameOverMenu.Hide();
    }

    public override void Hide(bool playAnimation = false)
    {
        base.Hide(playAnimation);

        _reanimationMenu.Hide(playAnimation);
        _gameOverMenu.Hide(playAnimation);
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

    #region Buttons
    public void OnCurrencyReanimation()
    {
        if (_mainInentory.GemsInventory.Spend(_reanimationMenu.ReanimationCost))
        {
            _player.GetUpgrade(_reanimationMenu.ReanimationByCurrencyHealUpgrade);
            _reanimations++;

            _mainMenu.DisplayDefault();
        }
        else
        {
            _mainMenu.ShowNotReadyMessage("Not enough resources!");
        }
    }

    public void OnAdReanimation()
    {
        _mainMenu.ShowNotReadyMessage("Ad is not ready");
    }

    public void OnCloseReanimation()
    {
        _reanimationMenu.Hide();
        _gameOverMenu.Display(true);
    }

    public void OnContinueGameOver()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    #endregion
}
