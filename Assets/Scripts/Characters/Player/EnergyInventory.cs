using UnityEngine;

[System.Serializable]
public class EnergyInventory : CurrencyInventory
{
    [SerializeField] private int _maxValue;

    public int MaxValue => _maxValue;

    public EnergyInventory(CurrencyData currencyData, EnergyCounter counter) : base(currencyData, counter)
    {
    }
}

