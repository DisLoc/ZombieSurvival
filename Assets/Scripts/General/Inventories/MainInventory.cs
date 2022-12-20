using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

public class MainInventory : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Inventories settings")]
    [SerializeField] private List<LevelContext> _levels;
    [SerializeField] private CurrencyInventory _coinInventory;
    [SerializeField] private CurrencyInventory _gemsInventory;
    [SerializeField] private CurrencyInventory _keysInventory;
    [SerializeField] private EnergyInventory _energyInventory;
    [SerializeField] private EquipmentInventory _equipmentInventory;
    [SerializeField] private CampInventory _campInventory;

    [SerializeField] private PlayerExpLevel _playerLevel;

    private EquipmentMaterialInventory _materialsInventory;

    public List<LevelContext> Levels => _levels;
    public CurrencyInventory CoinInventory => _coinInventory;
    public CurrencyInventory GemsInventory => _gemsInventory;
    public CurrencyInventory KeysInventory => _keysInventory;
    public EnergyInventory EnergyInventory => _energyInventory;
    public EquipmentInventory EquipmentInventory => _equipmentInventory;
    public CampInventory CampInventory => _campInventory;
    public EquipmentMaterialInventory MaterialsInventory => _materialsInventory;
    public PlayerExpLevel PlayerLevel => _playerLevel;

    private void OnEnable()
    {
        _playerLevel.Initialize();

        _coinInventory.Initialize();
        _gemsInventory.Initialize();
        _energyInventory.Initialize();

        _equipmentInventory.Initialize();
        _equipmentInventory.EquipmentList.SetIDs();
        _campInventory.Initialize();

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
        _coinInventory.LoadData(DataPath.Load(DataPath.CoinsInventory));
        _gemsInventory.LoadData(DataPath.Load(DataPath.GemsInvneotry));
        _keysInventory.LoadData(DataPath.Load(DataPath.KeysInventory));
        _energyInventory.LoadData(DataPath.Load(DataPath.EnergyInventory));
        _equipmentInventory.LoadData(DataPath.Load(DataPath.EquipmentInventory));
        _campInventory.LoadData(DataPath.Load(DataPath.CampInventory));
        _playerLevel.LoadData(DataPath.Load(DataPath.PlayerLevel));
        _materialsInventory.LoadData(DataPath.Load(DataPath.MaterialsInventory));

        foreach (LevelContext context in _levels)
        {
            context.LoadData();
        }
    }

    [ContextMenu("Save data")]
    public void SaveData()
    {
        DataPath.Save(DataPath.CoinsInventory, _coinInventory.SaveData());
        DataPath.Save(DataPath.GemsInvneotry, _gemsInventory.SaveData());
        DataPath.Save(DataPath.KeysInventory, _keysInventory.SaveData());
        DataPath.Save(DataPath.EnergyInventory, _energyInventory.SaveData());
        DataPath.Save(DataPath.EquipmentInventory, _equipmentInventory.SaveData());
        DataPath.Save(DataPath.CampInventory, _campInventory.SaveData());
        DataPath.Save(DataPath.PlayerLevel, _playerLevel.SaveData());
        DataPath.Save(DataPath.MaterialsInventory, _materialsInventory.SaveData());
        
        foreach (LevelContext context in _levels)
        {
            context.SaveData();
        }
    }

    [ContextMenu("Reset data")]
    private void ResetData()
    {
        if (File.Exists(DataPath.CoinsInventory))
        {
            File.Delete(DataPath.CoinsInventory);
            _coinInventory.ResetData();

            if (_isDebug) Debug.Log("Reset CoinsInventory");
        }
        
        if (File.Exists(DataPath.GemsInvneotry))
        {
            File.Delete(DataPath.GemsInvneotry);
            _gemsInventory.ResetData();

            if (_isDebug) Debug.Log("Reset GemsInvneotry");
        }

        if (File.Exists(DataPath.KeysInventory))
        {
            File.Delete(DataPath.KeysInventory);
            _keysInventory.ResetData();

            if (_isDebug) Debug.Log("Reset KeysInventory");
        }

        if (File.Exists(DataPath.EnergyInventory))
        {
            File.Delete(DataPath.EnergyInventory);
            _energyInventory.ResetData();

            if (_isDebug) Debug.Log("Reset EnergyInventory");
        }
        
        if (File.Exists(DataPath.EquipmentInventory))
        {
            File.Delete(DataPath.EquipmentInventory);
            _equipmentInventory.ResetData();

            if (_isDebug) Debug.Log("Reset EquipmentInventory");
        }
        
        if (File.Exists(DataPath.CampInventory))
        {
            File.Delete(DataPath.CampInventory);
            _campInventory.ResetData();

            if (_isDebug) Debug.Log("Reset CampInventory");
        }

        if (File.Exists(DataPath.MaterialsInventory))
        {
            File.Delete(DataPath.MaterialsInventory);
            //_materialsInventory.ResetData();

            if (_isDebug) Debug.Log("Reset MaterialsInventory");
        }
        
        if (File.Exists(DataPath.PlayerLevel))
        {
            File.Delete(DataPath.PlayerLevel);
            _playerLevel.ResetData();

            if (_isDebug) Debug.Log("Reset PlayerLevel");
        }        

        if (File.Exists(DataPath.SpecialGift))
        {
            File.Delete(DataPath.SpecialGift);

            if (_isDebug) Debug.Log("Reset SpecialGift");
        }
        
        if (File.Exists(DataPath.Supplies))
        {
            File.Delete(DataPath.Supplies);

            if (_isDebug) Debug.Log("Reset Supplies");
        }

        foreach (LevelContext context in _levels)
        {
            context.ResetLevel();
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

        inventory?.Add(currency);

        SaveData();
    }

    public bool Spend(Currency currency)
    {
        if (EnoughResources(currency, out CurrencyInventory inventory))
        {
            inventory.Spend(currency);

            SaveData();

            return true;
        }
        else return false;
    }

    public CurrencyInventory FindInventory(CurrencyData data)
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
        else if (data.Equals(_keysInventory.CurrencyData))
        {
            inventory = _keysInventory;
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
                return _equipmentInventory.IsEnough(equipmentData, materials.RequiredEquipmentAmount);
            }
        }

        return false;
    }

    public bool EnoughResources(Equipment equipment, int count = 1)
    {
        return _equipmentInventory.IsEnough(equipment, count);
    }

    public void Add(Equipment equipment, int count = 1)
    {
        if (_isDebug) Debug.Log("Add equipment: " + equipment.name + " x" + count);

        for (int i = 0; i < count; i++)
        {
            _equipmentInventory.Add(equipment);
        }

        SaveData();
    }

    public void Add(EquipmentMaterial material, int count = 1)
    {
        if (_isDebug) Debug.Log("Add material: " + material.name + " x" + count);

        _materialsInventory.Add(material.ValidEquipment, count);

        SaveData();
    }
    
    public bool Spend(Equipment equipment, int count = 1)
    {
        bool spended = _equipmentInventory.Spend(equipment, count);
        
        if (spended)
        {
            if (_isDebug) Debug.Log("Removed equipment: " + equipment.name + " x" + count);

            SaveData();

            return true;
        }

        return false;
    }
    
    public bool Spend(EquipmentMaterial material, int count = 1)
    {
        bool spended = _materialsInventory.Remove(material.ValidEquipment, count);
        
        if (spended)
        {
            if (_isDebug) Debug.Log("Removed material: " + material.name + " x" + count);

            SaveData();

            return true;
        }

        return false; 
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

    [ContextMenu("Add exp")]
    private void AddExpirience()
    {
        _playerLevel.AddExp(426);
    }

    [ContextMenu("Add equipment")]
    private void AddRandomEquipment()
    {
        _equipmentInventory.Add(_equipmentInventory.EquipmentList.GetRandomEquipment());
    }
    
    [ContextMenu("Add equipment material")]
    private void AddRandomEquipmentMaterial()
    {
        _materialsInventory.Add(_equipmentInventory.EquipmentList.GetRandomMaterial().ValidEquipment, 5);
    }
    #endregion

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
