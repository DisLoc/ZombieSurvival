using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Upgrades/Upgrade data", fileName = "New upgrade data")]
public class UpgradeData : ScriptableObject
{
    [SerializeField] protected UpgradeMarker _upgradingStatMarker;
    [SerializeField] protected float _upgradeValue = 0;
    [SerializeField][Range(0, 10)] protected float _upgradeMultiplier = 1;

    public UpgradeMarker UpgradingStatMarker => _upgradingStatMarker;
    public float UpgradeValue => _upgradeValue;
    public float UpgradeMultiplier => _upgradeMultiplier;
}
