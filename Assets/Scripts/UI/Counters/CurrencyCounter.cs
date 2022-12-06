using UnityEngine;
using UnityEngine.UI;

public class CurrencyCounter : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    [Header("Settings")]
    [SerializeField] protected Image _currencyImage;
    [SerializeField] protected Text _currencyText;

    protected CurrencyInventory _inventory;

    public CurrencyData CurrencyData => _inventory.CurrencyData;
    public int TotalGained => _inventory.Total;

    public virtual void Initialize(CurrencyInventory currencyInventory)
    {
        _inventory = currencyInventory;

        if (currencyInventory.CurrencyData.Icon != null)
        {
            _currencyImage.sprite = currencyInventory.CurrencyData.Icon;
        }

        UpdateCounter();
    }

    public virtual void UpdateCounter()
    {
        if (_inventory != null)
        {
            _currencyText.text = IntegerFormatter.GetCurrency(_inventory.Total);
        }
        else if (_isDebug) Debug.Log("Missing inventory!");
    }
}
