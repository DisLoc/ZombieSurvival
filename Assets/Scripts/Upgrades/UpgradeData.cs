using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Upgrades/Upgrade data", fileName = "New upgrade data")]
public class UpgradeData : ScriptableObject
{
    [SerializeField] protected MarkerList _upgradingMarkers;
    [SerializeField] protected float _upgradeValue = 0;
    [SerializeField][Range(0, 10)] protected float _upgradeMultiplier = 1;

    public MarkerList UpgradingMarkers => _upgradingMarkers;
    public float UpgradeValue => _upgradeValue;
    public float UpgradeMultiplier => _upgradeMultiplier;
}
