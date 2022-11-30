using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Equipment/Equipment material", fileName = "New equipment material")]
public class EquipmentMaterial : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private Sprite _background;
    [SerializeField] private EquipSlot _validEquipment;

    public Sprite Icon => _icon;
    public Sprite Background => _background;
    public EquipSlot ValidEquipment => _validEquipment;
}
