using UnityEngine;

[System.Serializable]
public class EnergyInventory : CurrencyInventory
{
    [SerializeField] private EnergyStat _energy;

    public EnergyStat Energy => _energy;

    public EnergyInventory(CurrencyData currencyData, EnergyCounter counter) : base(currencyData, counter) { }

    public override void Initialize()
    {
        _energy.Initialize();
        _total = (int)_energy.Value;

        if (_counter != null)
        {
            _counter.Initialize(this);
        }
    }

    public override void Add(Currency currency)
    {
        _energy.SetValue(_energy.Value + currency.CurrencyValue);

        _total = (int)_energy.Value;

        _counter.UpdateCounter();
    }

    public override bool Spend(Currency currency)
    {
        if (IsEnough(currency))
        {
            _energy.SetValue(_energy.Value - currency.CurrencyValue);

            _total = (int)_energy.Value;

            _counter.UpdateCounter();

            return true;
        }

        return false;
    }

    #region Serialization
    public override void LoadData(SerializableData data)
    {
        if (data == null) return;

        _energy.SetValue((data as CurrencyInventoryData).total);
        _total = (int)_energy.Value;

        _counter.UpdateCounter();
    }

    public override SerializableData SaveData()
    {
        CurrencyInventoryData data = new CurrencyInventoryData();

        _total = (int)_energy.Value;
        data.total = _total;

        return data;
    }

    public override void ResetData()
    {
        _energy.Initialize();

        base.ResetData();
    }
    #endregion
}

