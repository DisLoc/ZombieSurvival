using System.Collections.Generic;
using UnityEngine;

public class MainInventory : MonoBehaviour
{
    [Header("Inventories settings")]
    [SerializeField] private CurrencyInventory _coinInventory;
    [SerializeField] private CurrencyInventory _gemsInventory;
    [SerializeField] private EnergyInventory _energyInventory;
    [SerializeField] private EquipmentInventory _equipmentInventory;

    [SerializeField] private PlayerExpLevel _playerLevel;

    private EquipmentMaterialInventory _materialsInventory;

    public CurrencyInventory CoinInventory => _coinInventory;
    public CurrencyInventory GesmsInventory => _gemsInventory;
    public EnergyInventory EnergyInventory => _energyInventory;
    public EquipmentInventory EquipmentInventory => _equipmentInventory;

    [Header("Test")]
    [SerializeField] private bool _useBaseEquipment;
    [SerializeField] private List<Equipment> _baseEquipment;

    public bool UseBaseEquipment => _useBaseEquipment;
    public List<Equipment> BaseEquipment => _baseEquipment;

    private void OnEnable()
    {
        _playerLevel.Initialize();

        _coinInventory.Initialize();
        _gemsInventory.Initialize();
        _energyInventory.Initialize();

        _materialsInventory = new EquipmentMaterialInventory();

        LoadData();
    }

    private void OnDisable()
    {
        SaveData();
    }

    private void LoadData()
    {

    }

    private void SaveData()
    {
        SerializableData coinData = _coinInventory.SaveData();
        SerializableData gemData = _gemsInventory.SaveData();
        SerializableData energyData = _energyInventory.SaveData();
        SerializableData equipmentData = _equipmentInventory.SaveData();

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

    public void Add(Equipment equipment, int count = 1)
    {

    }

    public void Add(EquipmentMaterial material, int count = 1)
    {
        _materialsInventory.Add(material.ValidEquipment, count);
    }
    
    public bool Spend(Equipment equipment, int count = 1)
    {
        return true;
    }
    
    public bool Spend(EquipmentMaterial material, int count = 1)
    {
        return _materialsInventory.Remove(material.ValidEquipment, count);
    }
    #endregion
}
