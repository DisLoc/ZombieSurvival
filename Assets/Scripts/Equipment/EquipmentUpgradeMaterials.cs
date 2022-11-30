using UnityEngine;

[System.Serializable]
public class EquipmentUpgradeMaterials
{
    [SerializeField] private int _requiredEquipmentAmount;
    [SerializeField] private int _requiredCurrencyAmount;
    [SerializeField] private int _requiredMaterialAmount;

    public int RequiredEquipmentAmount => _requiredEquipmentAmount;
    public int RequiredCurrencyAmount => _requiredCurrencyAmount;
    public int RequiredMaterialAmount => _requiredMaterialAmount;
}
