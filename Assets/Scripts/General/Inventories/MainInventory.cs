using System.Collections.Generic;
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
        _coinInventory.LoadData(GameData.Load(GameData.CoinsInventory));
        _gemsInventory.LoadData(GameData.Load(GameData.GemsInvneotry));
        _keysInventory.LoadData(GameData.Load(GameData.KeysInventory));
        _energyInventory.LoadData(GameData.Load(GameData.EnergyInventory));
        _equipmentInventory.LoadData(GameData.Load(GameData.EquipmentInventory));
        _campInventory.LoadData(GameData.Load(GameData.CampInventory));
        _playerLevel.LoadData(GameData.Load(GameData.PlayerLevel));
        _materialsInventory.LoadData(GameData.Load(GameData.MaterialsInventory));

        foreach (LevelContext context in _levels)
        {
            context.LoadData();
        }
    }

    [ContextMenu("Save data")]
    public void SaveData()
    {
        GameData.Save(GameData.CoinsInventory, _coinInventory.SaveData());
        GameData.Save(GameData.GemsInvneotry, _gemsInventory.SaveData());
        GameData.Save(GameData.EnergyInventory, _energyInventory.SaveData());
        GameData.Save(GameData.KeysInventory, _keysInventory.SaveData());
        GameData.Save(GameData.EquipmentInventory, _equipmentInventory.SaveData());
        GameData.Save(GameData.CampInventory, _campInventory.SaveData());
        GameData.Save(GameData.MaterialsInventory, _materialsInventory.SaveData());
        GameData.Save(GameData.PlayerLevel, _playerLevel.SaveData());
        
        foreach (LevelContext context in _levels)
        {
            context.SaveData();
        }
    }

    public bool SaveInventory(Inventory inventory)
    {
        if (inventory.Equals(_coinInventory))
        {
            GameData.Save(GameData.CoinsInventory, _coinInventory.SaveData());
            return true;
        }
        else if (inventory.Equals(_gemsInventory))
        {
            GameData.Save(GameData.GemsInvneotry, _gemsInventory.SaveData());
            return true;
        }
        else if (inventory.Equals(_energyInventory))
        {
            GameData.Save(GameData.EnergyInventory, _energyInventory.SaveData());
            return true;
        }
        else if (inventory.Equals(_keysInventory))
        {
            GameData.Save(GameData.KeysInventory, _keysInventory.SaveData());
            return true;
        }
        else if (inventory.Equals(_equipmentInventory))
        {
            GameData.Save(GameData.EquipmentInventory, _equipmentInventory.SaveData());
            return true;
        }
        else if (inventory.Equals(_campInventory))
        {
            GameData.Save(GameData.CampInventory, _campInventory.SaveData());
            return true;
        }
        else if (inventory.Equals(_materialsInventory))
        {
            GameData.Save(GameData.MaterialsInventory, _materialsInventory.SaveData());
            return true;
        }

        return false;
    }

    public void SavePlayer()
    {
        GameData.Save(GameData.PlayerLevel, _playerLevel.SaveData());
    }

    [ContextMenu("Reset data")]
    private void ResetData()
    {
        if (File.Exists(GameData.CoinsInventory))
        {
            File.Delete(GameData.CoinsInventory);
            _coinInventory.ResetData();

            if (_isDebug) Debug.Log("Reset CoinsInventory");
        }
        
        if (File.Exists(GameData.GemsInvneotry))
        {
            File.Delete(GameData.GemsInvneotry);
            _gemsInventory.ResetData();

            if (_isDebug) Debug.Log("Reset GemsInvneotry");
        }

        if (File.Exists(GameData.KeysInventory))
        {
            File.Delete(GameData.KeysInventory);
            _keysInventory.ResetData();

            if (_isDebug) Debug.Log("Reset KeysInventory");
        }

        if (File.Exists(GameData.EnergyInventory))
        {
            File.Delete(GameData.EnergyInventory);
            _energyInventory.ResetData();

            if (_isDebug) Debug.Log("Reset EnergyInventory");
        }
        
        if (File.Exists(GameData.EquipmentInventory))
        {
            File.Delete(GameData.EquipmentInventory);
            _equipmentInventory.ResetData();

            if (_isDebug) Debug.Log("Reset EquipmentInventory");
        }
        
        if (File.Exists(GameData.CampInventory))
        {
            File.Delete(GameData.CampInventory);
            _campInventory.ResetData();

            if (_isDebug) Debug.Log("Reset CampInventory");
        }

        if (File.Exists(GameData.MaterialsInventory))
        {
            File.Delete(GameData.MaterialsInventory);
            //_materialsInventory.ResetData();

            if (_isDebug) Debug.Log("Reset MaterialsInventory");
        }
        
        if (File.Exists(GameData.PlayerLevel))
        {
            File.Delete(GameData.PlayerLevel);
            _playerLevel.ResetData();

            if (_isDebug) Debug.Log("Reset PlayerLevel");
        }        

        if (File.Exists(GameData.SpecialGift))
        {
            File.Delete(GameData.SpecialGift);

            if (_isDebug) Debug.Log("Reset SpecialGift");
        }
        
        if (File.Exists(GameData.Supplies))
        {
            File.Delete(GameData.Supplies);

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

        SaveInventory(inventory);
    }

    public bool Spend(Currency currency)
    {
        if (EnoughResources(currency, out CurrencyInventory inventory))
        {
            inventory.Spend(currency);

            SaveInventory(inventory);

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

        SaveInventory(_equipmentInventory);
    }

    public void Add(EquipmentMaterial material, int count = 1)
    {
        if (_isDebug) Debug.Log("Add material: " + material.name + " x" + count);

        _materialsInventory.Add(material.ValidEquipment, count);

        SaveInventory(_materialsInventory);
    }
    
    public bool Spend(Equipment equipment, int count = 1)
    {
        bool spended = _equipmentInventory.Spend(equipment, count);
        
        if (spended)
        {
            if (_isDebug) Debug.Log("Removed equipment: " + equipment.name + " x" + count);

            SaveInventory(_equipmentInventory);

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

            SaveInventory(_materialsInventory);

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


    [ContextMenu("Add gems")]
    private void AddGems()
    {
        _gemsInventory.Add(new Currency(_gemsInventory.CurrencyData, 50));
    }


    [ContextMenu("Add energy")]
    private void AddEnergy()
    {
        _energyInventory.Add(new Currency(_energyInventory.CurrencyData, 20));
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
        _materialsInventory.Add(_equipmentInventory.EquipmentList.GetRandomMaterial().ValidEquipment, 20);
    }
    #endregion
}
