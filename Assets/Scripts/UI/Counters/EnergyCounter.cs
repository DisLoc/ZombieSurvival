using UnityEngine;
using UnityEngine.UI;

public class EnergyCounter : CurrencyCounter
{
    [SerializeField] private Text _maxCurrencyText;

    public override void UpdateCounter()
    {
        base.UpdateCounter();

        if (_inventory as EnergyInventory != null)
        {
            _maxCurrencyText.text = IntegerFormatter.GetCurrency((_inventory as EnergyInventory).MaxValue);
        }
        else if (_isDebug) Debug.Log("Missing inventory!");
    }
        
}
