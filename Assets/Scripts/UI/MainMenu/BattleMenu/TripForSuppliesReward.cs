using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TripForSuppliesReward
{
    [SerializeField] private int _requiredLevel;

    [Space(5)]
    [SerializeField] private CurrencyReward _goldPerTick;
    [SerializeField] private ExpirienceReward _expPerTick;

    [Space(5)]
    [SerializeField] private MaterialReward _materialReward;
    [Tooltip("Time in seconds")]
    [SerializeField] private int _requiredTimeForMaterial;

    [Space(5)]
    [SerializeField] private EquipmentReward _equipmentReward;
    [Tooltip("Time in seconds")]
    [SerializeField] private int _requiredTimeForEquipment;

    
    public bool initialized { get; private set; }

    public int RequiredLevel => _requiredLevel;

    public CurrencyReward GoldPerTick => _goldPerTick;
    public ExpirienceReward ExpPerTick => _expPerTick;

    public MaterialReward MaterialReward => _materialReward;
    public int RequiredTimeForMaterial => _requiredTimeForMaterial;

    public EquipmentReward EquipmentReward => _equipmentReward;
    public int RequiredTimeForEquipment => _requiredTimeForEquipment;
}