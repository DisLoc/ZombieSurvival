using UnityEngine;
using UnityEngine.UI;

public class PopupHint : UIMenu
{
    [Header("Hint settings")]
    [SerializeField] private Image _currencyIcon;
    [SerializeField] private Text _currencyText;

    public void Show(Currency currency)
    {
        _currencyIcon.sprite = currency.CurrencyData.Icon;
        _currencyText.text = currency.CurrencyValue.ToString();
    }

    public void Cancel()
    {
        (_parentMenu as CampTalentsMenu).HideHint();
    }

    public void Unlock()
    {
        (_parentMenu as CampTalentsMenu).Unlock();
        (_parentMenu as CampTalentsMenu).HideHint();
    }
}
