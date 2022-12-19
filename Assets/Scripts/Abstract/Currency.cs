using UnityEngine;

[System.Serializable]
public class Currency
{
    [SerializeField][Range(0, 100000)] protected int _currencyValue;
    [SerializeField] protected CurrencyData _currencyData;
    public int CurrencyValue => _currencyValue;
    public CurrencyData CurrencyData => _currencyData;

    public Currency (CurrencyData data, int value)
    {
        _currencyData = data;
        _currencyValue = value;
    }

    public static Currency operator *(Currency currency, int multiplier)
    {
        return new Currency(currency._currencyData, currency._currencyValue * multiplier);
    }
    
    public static Currency operator *(Currency currency, float multiplier)
    {
        return new Currency(currency._currencyData, (int)(currency._currencyValue * multiplier));
    }
}
