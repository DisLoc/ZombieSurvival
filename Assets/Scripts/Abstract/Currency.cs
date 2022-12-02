using UnityEngine;

public class Currency
{
    [SerializeField][Range(0, 100000)] protected int _currencyValue;
    [SerializeField] protected CurrencyData _currencyData;

    public Currency (CurrencyData data, int value)
    {
        _currencyData = data;
        _currencyValue = value;
    }

    public int CurrencyValue => _currencyValue;
    public CurrencyData CurrencyData => _currencyData;
}
