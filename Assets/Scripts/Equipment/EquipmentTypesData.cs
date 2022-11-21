using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Equipment/Equipment types data", fileName = "New equipment types data")]
public sealed class EquipmentTypesData : ScriptableObject
{
    [SerializeField] private List<EquipmentRarityBackground> _rarityBackgrounds;
    [SerializeField] private List<EquipmentSlotIcon> _slotIcons;

    public EquipmentRarityBackground this[EquipRarity rarity] => _rarityBackgrounds.Find(item => item.Rarity.Equals(rarity));

    public EquipmentSlotIcon this[EquipSlot slot] => _slotIcons.Find(item => item.Slot.Equals(slot));

    [System.Serializable]
    public sealed class EquipmentRarityBackground
    {
        [SerializeField] private EquipRarity _rarity;
        [SerializeField] private Sprite _rarityBackground;
        [SerializeField] private Sprite _rarityCircle;

        public EquipRarity Rarity => _rarity;
        public Sprite RarityBackground => _rarityBackground;
        public Sprite RarityCircle => _rarityCircle;
    }

    [System.Serializable]
    public sealed class EquipmentSlotIcon
    {
        [SerializeField] private EquipSlot _slot;
        [SerializeField] private Sprite _slotIcon;
        
        public EquipSlot Slot => _slot;
        public Sprite SlotIcon => _slotIcon;
    }
}
