using UnityEngine;

public abstract class AbilityUpgradeData : ScriptableObject
{
    [SerializeField] protected string _name;
    [Tooltip("Icon displays in inventory")]
    [SerializeField] protected Sprite _inventoryIcon;
    [Tooltip("Icon displays when choose abilities")]
    [SerializeField] protected Sprite _upgradeIcon;

    public string Name => _name;
    public Sprite InventoryIcon => _inventoryIcon;
    public Sprite UpgradeIcon => _upgradeIcon;
}
