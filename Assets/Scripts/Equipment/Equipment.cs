using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Equipment/Equipment", fileName = "New equipment")]
public class Equipment : ScriptableObject
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Equipment settings")]
    [SerializeField] private Sprite _icon;
    [SerializeField] private EquipSlot _equipSlot;
    [SerializeField] private EquipRarity _rarity;
    [SerializeField] private UpgradedStat _upgradingStat;
    [SerializeField] private EquipmentUpgrades _equipUpgrade;

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
                return _previousRarityEquipment.EquipUpgrade + _rarityUpgrade + _equipUpgrade.GetUpgrade((int)_level.Value);
            }
            else
            {
                return _rarityUpgrade + _equipUpgrade.GetUpgrade((int)_level.Value);
            }
        }
    }

    public void Initialize()
    {
        _level.Initialize();
    }
}
