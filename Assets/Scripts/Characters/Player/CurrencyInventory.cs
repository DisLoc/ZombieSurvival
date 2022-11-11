using UnityEngine;

[System.Serializable]
public class CurrencyInventory
{ 
    [SerializeField] private CurrencyData _currencyData;
    [SerializeField] private CurrencyCounter _counter;

    private int _total;

    public CurrencyData CurrencyData => _currencyData;
    public int Total => _total;

    public CurrencyInventory(CurrencyData currencyData, CurrencyCounter counter)
    {
        _currencyData = currencyData;
        _counter = counter;
        _total = 0;

        _counter.Initialize(this);
    }

    public void Initialize()
    {
        _counter.Initialize(this);
    }

    public void Add(Currency currency)
    {
        _total += currency.CurrencyValue;

        if (_counter != null)
        {
            _counter.UpdateCounter();
        }
    }

    public bool Spend(Currency currency)
    {
        if (IsEnough(currency))
        {
            _total -= currency.CurrencyValue;

            if (_counter != null)
            {
                _counter.UpdateCounter();
            }

            return true;
        }
        else return false;
    }

    public bool IsEnough(Currency currency)
    {
        return  currency.CurrencyValue <= _total;
    }

    public void GetUpgrade(Upgrade upgrade)
    {
        if (upgrade.Upgrades != null && upgrade.Upgrades.Count > 0)
        {
            foreach (UpgradeData data in upgrade.Upgrades)
            {
                if (data.UpgradingStatMarker.Equals(_currencyData.Marker))
                {
                    _total = (int)((_total + data.UpgradeValue) * data.UpgradeMultiplier);

                    if (_counter != null)
                    {
                        _counter.UpdateCounter();
                    }
                }
            }
        }
    }
}
