using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    [Header("Settings")]
    [SerializeField] protected UIMenu _defaultMenu;
    [SerializeField] protected List<UIMenu> _menus;

    protected virtual void OnEnable()
    {
        foreach (UIMenu menu in _menus)
        {
            menu.Initialize(this);

            if (menu.Equals(_defaultMenu))
            {
                menu.Display();
            }

            else menu.Hide();
        }
    }

    public virtual void Display(UIMenu tab)
    {
        foreach (UIMenu menu in _menus)
        {
            if (menu.Equals(tab))
            {
                menu.Display();
            }

            else menu.Hide();
        }
    }

    public void DisplayDefault()
    {
        Display(_defaultMenu);
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
