using UnityEngine;

public class Currency
{
    [SerializeField][Range(0, 1000)] protected int _currencyValue;
    [SerializeField] protected CurrencyData _currencyData;

    public int CurrencyValue => _currencyValue;
    public CurrencyData CurrencyData => _currencyData;
}
