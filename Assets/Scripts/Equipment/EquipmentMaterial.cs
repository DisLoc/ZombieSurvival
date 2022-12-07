using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Equipment/Equipment material", fileName = "New equipment material")]
public class EquipmentMaterial : CurrencyData
{
    [SerializeField] private EquipSlot _validEquipment;

    public EquipSlot ValidEquipment => _validEquipment;
}
