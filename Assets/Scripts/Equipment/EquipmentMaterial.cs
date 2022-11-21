using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Equipment/Equipment material", fileName = "New equipment material")]
public class EquipmentMaterial : ScriptableObject
{
    [SerializeField] private Sprite _icon;

    public Sprite Icon => _icon;
}
