using UnityEngine;

public class EnergyCounter : CurrencyCounter
{
    public override void UpdateCounter()
    {
        if (_inventory is EnergyInventory inventory)
        {
            _currencyText.text = ((int)inventory.Energy.Value).ToString() + "/" + ((int)inventory.Energy.MaxValue).ToString();
        }
        else if (_isDebug) Debug.Log("Missing inventory!");
    }
        
}
