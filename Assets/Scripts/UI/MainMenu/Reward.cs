using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Reward
{
    [SerializeField] private int _expirience;
    [SerializeField] private List<Currency> _currencyRewards;

    [Space(5)]
    [SerializeField] private bool _hasRandomMaterialReward;
    [Tooltip("If HasRandomMaterialReward is true, field can be null")]
    [SerializeField] private EquipmentMaterial _specificMaterialReward;
    [SerializeField] private int _materialsCount = 1;

    [Space(5)]
    [SerializeField] private bool _hasRandomEquipmentReward;
    [Tooltip("If HasRandomEquipmentReward is true, must fill that field")]
    [SerializeField] private EquipRarity _specificRandomEquipmentRarity;
    [Tooltip("If HasRandomEquipmentReward is true, field can be null")]
    [SerializeField] private Equipment _specificEquipmentReward;
    [SerializeField] private int _equipmentCount = 1;

    public int Expirience => _expirience;
    public List<Currency> CurrencyRewards => _currencyRewards;

    public bool HasRandomMaterialReward => _hasRandomMaterialReward;
    public EquipmentMaterial SpecificMaterialReward => _specificMaterialReward;
    public int MaterialsCount => _materialsCount;
    
    public bool HasRandomEquipmentReward => _hasRandomEquipmentReward;
    public EquipRarity EquipmentRarity => _specificRandomEquipmentRarity;
    public Equipment SpecificEquipmentReward => _specificEquipmentReward;
    public int EquipmentCount => _equipmentCount;
}
