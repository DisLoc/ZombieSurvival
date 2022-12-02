using System.Collections;
using UnityEngine;

[System.Serializable]
public class MainInventory
{
    [SerializeField] private CurrencyInventory _coinInventory;
    [SerializeField] private CurrencyInventory _gemsInventory;
    [SerializeField] private EnergyInventory _energyInventory;

    [SerializeField] private InventoryMenu _equipmentMenu;

    [SerializeField] private PlayerExpBar _playerExpBar;

    //[SerializeField] private List<CurrencyInventory> _moreCurrencies;

    private EquipmentMaterialInventory _materialsInventory;

    public CurrencyInventory CoinInventory => _coinInventory;
    public CurrencyInventory GesmsInventory => _gemsInventory;
    public EnergyInventory EnergyInventory => _energyInventory;

    public void Initialize()
    {
        _coinInventory.Initialize();
        _gemsInventory.Initialize();
        _energyInventory.Initialize();

        _materialsInventory = new EquipmentMaterialInventory();
    }

    #region Currencies
    public bool EnoughResources(Currency currency)
    {
        CurrencyInventory inventory = FindInventory(currency.CurrencyData);

        if (inventory == null) return false;

        return inventory.IsEnough(currency);
    }

    private bool EnoughResources(Currency currency, out CurrencyInventory inventory)
    {
        inventory = FindInventory(currency.CurrencyData);

        if (inventory == null) return false;

        return inventory.IsEnough(currency);
    }

    public void Add(Currency currency)
    {
        CurrencyInventory inventory = FindInventory(currency.CurrencyData);

        inventory.Add(currency);
    }

    public bool Spend(Currency currency)
    {
        if (EnoughResources(currency, out CurrencyInventory inventory))
        {
            return inventory.Spend(currency);
        }
        else return false;
    }

    private CurrencyInventory FindInventory(CurrencyData data)
    {
        CurrencyInventory inventory;

        if (data.Equals(_coinInventory.CurrencyData))
        {
            inventory = _coinInventory;
        }
        else if (data.Equals(_gemsInventory.CurrencyData))
        {
            inventory = _gemsInventory;
        }
        else if (data.Equals(_energyInventory.CurrencyData))
        {
            inventory = _energyInventory;
        }
        else
        {
            Debug.Log("Missing data!");

            return null;
        }

        return inventory;
    }
    #endregion

    #region Equipment
    public bool EnoughResources(EquipmentUpgradeMaterials materials, EquipmentData equipmentData)
    {
        if (EnoughResources(new Currency(equipmentData.EquipmentUpgrades.RequiredCurrency, materials.RequiredCurrencyAmount)))
        {
            if (_materialsInventory.IsEnough(equipmentData.EquipmentUpgrades.RequiredMaterial.ValidEquipment, materials.RequiredMaterialAmount))
            {
                //if (EnoughEquipment)

                return true;
            }
        }

        return false;
    }

    public void Add(Equipment equipment)
    {

    }
    
    public void Remove(Equipment equipment)
    {

    }
    #endregion
}
