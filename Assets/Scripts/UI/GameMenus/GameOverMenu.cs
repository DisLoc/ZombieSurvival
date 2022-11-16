using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public sealed class GameOverMenu : UIMenu, IPlayerDieHandler
{
    [Header("Game over menu settings")]
    [SerializeField] private GOGameOverMenu _gameOverMenu;
    [SerializeField] private GOReanimationMenu _reanimationMenu;

    private int _reanimations;

    [Inject] private Player _player;

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
        _player.GetUpgrade(_reanimationMenu.ReanimationByCurrencyHealUpgrade);
        _reanimations++;

        _mainMenu.DisplayDefault();
    }

    public void OnAdReanimation()
    {
        return;

        /*
        _player.GetUpgrade(_reanimationMenu.ReanimationByAdHealUpgrade);
        _reanimations++;

        _mainMenu.DisplayDefault();
         */
    }

    public void OnCloseReanimation()
    {
        _reanimationMenu.Hide();
        _gameOverMenu.Display(true);
    }

    public void OnContinueGameOver()
    {
        SceneManager.LoadScene(0);
    }
    #endregion
}
