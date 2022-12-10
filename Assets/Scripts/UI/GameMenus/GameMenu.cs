using UnityEngine;
using Zenject;

public class GameMenu : MainMenu
{
    [SerializeField] private CurrencyCounter _currencyCounter;
    [SerializeField] private SurvivalTimeCounter _survivalTimeCounter;

    [Inject] private LevelContext _levelContext;
    [Inject] private MainInventory _mainInventory;

    public override void Display(UIMenu tab)
    {
        if (tab != _defaultMenu)
        {
            StopTime();
        }
        else
        {
            RestoreTime();
        }

        foreach (UIMenu menu in _menus)
        {
            if (menu.Equals(tab))
            {
                
                menu.Display(menu != _defaultMenu);
            }

            else menu.Hide();
        }
    }

    public void SaveLevel()
    {
        _mainInventory.Add(new Currency(_currencyCounter.CurrencyData, _currencyCounter.TotalGained));

        if ((int)_survivalTimeCounter.SurvivalTime > _levelContext.maxSurvivalTime)
            _levelContext.maxSurvivalTime = (int)_survivalTimeCounter.SurvivalTime;
    }

    private void StopTime()
    {
        if (_isDebug) Debug.Log("Time stops now");
        Time.timeScale = 0;
    }

    private void RestoreTime()
    {
        if (_isDebug) Debug.Log("Time restored from now");
        Time.timeScale = 1;
    }
}
