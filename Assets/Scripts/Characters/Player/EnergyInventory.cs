using UnityEngine;

[System.Serializable]
public class EnergyInventory : CurrencyInventory
{
    [SerializeField] private EnergyStat _energy;

    public EnergyStat Energy => _energy;

    public override void Initialize()
    {
        _energy.Initialize();

        base.Initialize();
    }

    public EnergyInventory(CurrencyData currencyData, EnergyCounter counter) : base(currencyData, counter)
    {

    }

    public override SerializableData SaveData()
    {
        return null;
    }

    public override void LoadData(SerializableData data)
    {

    }
}

