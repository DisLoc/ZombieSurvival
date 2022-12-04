using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainInventory : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

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

    #region Serializing

    [ContextMenu("Load data")]
    private void LoadData()
    {
        _coinInventory.LoadData(Load(DataPath.CoinsInventory));
        _gemsInventory.LoadData(Load(DataPath.GemsInvneotry));
        _energyInventory.LoadData(Load(DataPath.EnergyInventory));
        _equipmentInventory.LoadData(Load(DataPath.EquipmentInventory));
    }

    [ContextMenu("Save data")]
    private void SaveData()
    {
        Save(DataPath.CoinsInventory, _coinInventory.SaveData());
        Save(DataPath.GemsInvneotry, _gemsInventory.SaveData());
        Save(DataPath.EnergyInventory, _energyInventory.SaveData());
        Save(DataPath.EquipmentInventory, _equipmentInventory.SaveData());
    }

    private void Save(string path, SerializableData data)
    {
        if (data == null) return;

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(path);

        bf.Serialize(file, data);
        file.Close();

        if (_isDebug) Debug.Log("Data saved to " + path);
    }

    private SerializableData Load(string path)
    {
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);

            SerializableData data = (SerializableData)bf.Deserialize(file);

            file.Close();

            if (_isDebug) Debug.Log("Loaded data from " + path + ". " + data);

            return data;
        }
        else
        {
            if (_isDebug) Debug.Log("No data to load from " + path);

            return null;
        }
    }

    [ContextMenu("Reset data")]
    private void ResetData()
    {
        if (File.Exists(DataPath.CoinsInventory))
        {
            File.Delete(DataPath.CoinsInventory);
            _coinInventory.ResetData();
        }
        
        if (File.Exists(DataPath.GemsInvneotry))
        {
            File.Delete(DataPath.GemsInvneotry);
            _gemsInventory.ResetData();
        }
        
        if (File.Exists(DataPath.EnergyInventory))
        {
            File.Delete(DataPath.EnergyInventory);
            _energyInventory.ResetData();
        }
        
        if (File.Exists(DataPath.EquipmentInventory))
        {
            File.Delete(DataPath.EquipmentInventory);
            _equipmentInventory.ResetData();
        }
    }
    #endregion

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

    #region test
    [ContextMenu("Add coins")]
    private void AddCoins()
    {
        _coinInventory.Add(new Currency(_coinInventory.CurrencyData, 5000));
    }
    
    [ContextMenu("Spend coins")]
    private void SpendCoins()
    {
        _coinInventory.Spend(new Currency(_coinInventory.CurrencyData, 312));
    }


    [ContextMenu("Add gems")]
    private void AddGems()
    {
        _gemsInventory.Add(new Currency(_gemsInventory.CurrencyData, 50));
    }

    [ContextMenu("Spend gems")]
    private void SpendGems()
    {
        _gemsInventory.Spend(new Currency(_gemsInventory.CurrencyData, 8));
    }


    [ContextMenu("Add energy")]
    private void AddEnergy()
    {
        _energyInventory.Add(new Currency(_energyInventory.CurrencyData, 5));
    }

    [ContextMenu("Spend energy")]
    private void SpendEnergy()
    {
        _energyInventory.Spend(new Currency(_energyInventory.CurrencyData, 10));
    }
    #endregion
}
