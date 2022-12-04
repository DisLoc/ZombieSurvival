using UnityEngine;

[System.Serializable]
public class CurrencyInventory : Inventory
{
    [SerializeField] protected CurrencyData _currencyData;
    [SerializeField] protected CurrencyCounter _counter;

    protected int _total;

    public CurrencyData CurrencyData => _currencyData;
    public int Total => _total;

    public CurrencyInventory(CurrencyData currencyData, CurrencyCounter counter)
    {
        _currencyData = currencyData;
        _counter = counter;
        _total = 0;

        _counter.Initialize(this);
    }

    public virtual void Initialize()
    {
        _total = 0;

        if (_counter != null)
        {
            _counter.Initialize(this);
        }
    }

    public override SerializableData SaveData()
    {
        CurrencyInventoryData data = new CurrencyInventoryData();

        data.total = _total;

        return data;
    }

    public override void LoadData(SerializableData data)
    {
        if (data == null) return;

        _total = (data as CurrencyInventoryData).total;

        _counter.UpdateCounter();
    }

    public override void ResetData()
    {
        _total = 0;
        _counter.UpdateCounter();
    }

    public virtual void Add(Currency currency)
    {
        _total += currency.CurrencyValue;

        if (_counter != null)
        {
            _counter.UpdateCounter();
        }
    }

    public virtual bool Spend(Currency currency)
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
        return currency.CurrencyValue <= _total;
    }

    public void GetUpgrade(Upgrade upgrade)
    {
        if (upgrade.Upgrades != null && upgrade.Upgrades.Count > 0)
        {
            foreach (UpgradeData data in upgrade.Upgrades)
            {
                if (data.UpgradingMarkers.IsStrike(_currencyData.Marker))
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

    [System.Serializable]
    protected class CurrencyInventoryData : SerializableData
    {
        public int total;
    }
}
