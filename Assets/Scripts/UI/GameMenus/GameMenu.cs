using UnityEngine;

public class GameMenu : MainMenu
{
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
