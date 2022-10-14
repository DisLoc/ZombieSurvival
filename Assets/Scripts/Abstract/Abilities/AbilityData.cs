using UnityEngine;

public abstract class AbilityData : ScriptableObject
{
    [SerializeField] protected string _name;
    [SerializeField] protected Sprite _inventoryIcon;
    [SerializeField] protected Sprite _upgradeIcon;

    public string Name => _name;
    public Sprite InventoryIcon => _inventoryIcon;
    public Sprite UpgradeIcon => _upgradeIcon;
    public abstract AbilityStats Stats { get; }
}
