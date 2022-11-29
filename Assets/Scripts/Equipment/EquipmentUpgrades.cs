using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipmentUpgrades
{
    [SerializeField] private Equipment _requiredEquipment;
    [SerializeField] private CurrencyData _requiredCurrency;
    [SerializeField] private EquipmentMaterial _requiredMaterial;

    [SerializeField] private List<EquipmentUpgrade> _upgrades;

    public Equipment RequiredEquipment => _requiredEquipment;
    public CurrencyData RequiredCurrency => _requiredCurrency;
    public EquipmentMaterial RequiredMaterial => _requiredMaterial;

    public EquipmentUpgrade GetUpgrade(int level)
    {
        return _upgrades.Find(item => item.RequiredLevel == level);
    }
}
