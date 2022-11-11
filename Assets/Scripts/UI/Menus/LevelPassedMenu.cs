using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class LevelPassedMenu : UIMenu, ILevelPassedHandler
{
    [Header("Level passed menu settings")]
    [SerializeField] private Text _totalKilledText;
    [SerializeField] private Text _goldGainedText;

    [SerializeField] private CurrencyCounter _currencyCounter;
    [SerializeField] private ZombieCounter _enemyCounter;

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

        _totalKilledText.text = "0";
        _goldGainedText.text = "0";
    }

    public void OnLevelPassed()
    {
        Time.timeScale = 0;

        _mainMenu.Display(this);

        Display(true);

        _totalKilledText.text = _enemyCounter.TotalKilled.ToString();
        _goldGainedText.text = _currencyCounter.TotalGained.ToString();
    }

    public void OnContinue()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
