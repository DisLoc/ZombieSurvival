using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Equipment/Equipment data", fileName = "New equipment data")]
public class EquipmentData : ScriptableObject
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Equipment settings")]
    [SerializeField] private Sprite _icon;
    [SerializeField] private EquipSlot _equipSlot;
    [SerializeField] private EquipRarity _rarity;
    [SerializeField] private UpgradingStat _upgradingStat;
    [SerializeField] private EquipmentUpgrades _equipmentUpgrades;

    [Header("Rarity settings")]
    [SerializeField] private Upgrade _rarityUpgrade;
    [SerializeField] private EquipmentData _previousRarityEquipment;
    [SerializeField] private EquipmentData _nextRarityEquipment;

    public Sprite Icon => _icon;
    public EquipSlot EquipSlot => _equipSlot;
    public EquipRarity EquipRarity => _rarity;
    public UpgradingStat UpgradingStat => _upgradingStat;
    public EquipmentUpgrades EquipmentUpgrades => _equipmentUpgrades;

    public Upgrade RarityUpgrade => _rarityUpgrade;
    public EquipmentData PreviousRarityEquipment => _previousRarityEquipment;
    public EquipmentData NextRarityEquipment => _nextRarityEquipment;
}
