using UnityEngine;

[System.Serializable]
public class EquipmentUpgradeMaterials
{
    [SerializeField] private CurrencyData _requiredCurrency;
    [SerializeField] private int _requiredCurrencyAmount;

    [Space(5)]
    [SerializeField] private EquipmentMaterial _requiredMaterial;
    [SerializeField] private int _requiredMaterialAmount;

    public CurrencyData RequiredCurrency => _requiredCurrency;
    public int RequiredCurrencyAmount => _requiredCurrencyAmount;
    public EquipmentMaterial RequiredMaterial => _requiredMaterial;
    public int RequiredMaterialAmount => _requiredMaterialAmount;
}
