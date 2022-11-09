using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private UIMenu _defaultMenu;
    [SerializeField] private List<UIMenu> _menus;

    private void OnEnable()
    {
        foreach (var menu in _menus)
        {
            menu.Initialize(this);

            if (menu.Equals(_defaultMenu))
            {
                menu.Display();
            }

            else menu.Hide();
        }
    }

    public void Display(UIMenu tab)
    {
        foreach (var menu in _menus)
        {
            if (menu.Equals(tab))
            {
                menu.Display();
            }

            else menu.Hide();
        }
    }

    public void OnSettingsClick()
    {

    }

    public void OnBuyEnergyClick()
    {

    }

    public void OnBuyGemsClick()
    {

    }

    public void OnBuyGoldClick()
    {

    }
}
