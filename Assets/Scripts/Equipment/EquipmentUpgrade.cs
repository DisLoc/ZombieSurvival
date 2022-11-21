using UnityEngine;

[System.Serializable]
public struct EquipmentUpgrade
{
    [SerializeField] private int _requiredLevel;
    [SerializeField] private UpgradeData _upgradeData;
    [SerializeField] private EquipmentUpgradeMaterials _upgradeMaterials;

    public int RequiredLevel => _requiredLevel;
    public UpgradeData UpgradeData => _upgradeData;
    public EquipmentUpgradeMaterials UpgradeMaterials => _upgradeMaterials;
}