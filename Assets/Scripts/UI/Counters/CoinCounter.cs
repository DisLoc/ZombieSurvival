using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private Image _coinImage;
    [SerializeField] private Text _coinText;

    private CurrencyInventory _inventory;

    public void Initialize(CurrencyInventory currencyInventory)
    {
        _inventory = currencyInventory;

        if (currencyInventory.CurrencyData.Icon != null)
        {
            _coinImage.sprite = currencyInventory.CurrencyData.Icon;
        }
    }

    public void UpdateCounter()
    {
        if (_inventory != null)
        {
            _coinText.text = _inventory.Total.ToString();
        }
        else if (_isDebug) Debug.Log("Missing inventory!");
    }
}
