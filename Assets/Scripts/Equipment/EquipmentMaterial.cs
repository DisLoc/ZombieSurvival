using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Equipment/Equipment material", fileName = "New equipment material")]
public class EquipmentMaterial : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private EquipSlot _validEquipment;

    public Sprite Icon => _icon;
    public EquipSlot ValidEquipment => _validEquipment;
}
