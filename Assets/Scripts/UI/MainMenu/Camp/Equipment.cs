using UnityEngine;

public class Equipment : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Equipment settings")]
    [SerializeField] private Sprite _icon;
    [SerializeField] private EquipSlot _equipSlot;
    [SerializeField] private EquipRarity _rarity;
    [SerializeField] private UpgradedStat _upgradingStat;
    [SerializeField] private Upgrade _equipUpgrade;

    [Header("Rarity settings")]
    [SerializeField] private Level _level;
    [SerializeField] private Upgrade _rarityUpgrade;
    [SerializeField] private Equipment _previousRarityEquipment;

    public Sprite Icon => _icon;
    public EquipSlot EquipSlot => _equipSlot;
    public EquipRarity EquipRarity => _rarity;
    public UpgradedStat UpgradingStat => _upgradingStat;

    public Level Level => _level;

    public Upgrade EquipUpgrade
    {
        get
        {
            if (_previousRarityEquipment != null)
            {
                return _rarityUpgrade + _equipUpgrade + _previousRarityEquipment.EquipUpgrade;
            }
            else return _rarityUpgrade + _equipUpgrade;
        }
    }
}
