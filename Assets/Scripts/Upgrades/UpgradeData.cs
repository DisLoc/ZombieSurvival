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

    public UpgradeData (MarkerList upgradingMarkers, float upgradeValue, float upgradeMultiplier)
    {
        _upgradingMarkers = upgradingMarkers;
        _upgradeValue = upgradeValue;
        _upgradeMultiplier = upgradeMultiplier;
    }

    public static UpgradeData operator *(UpgradeData data, int multiplier)
    {
        return new UpgradeData (data._upgradingMarkers, data._upgradeValue * multiplier, data._upgradeMultiplier);
    }
    
    public static UpgradeData operator *(UpgradeData data, float multiplier)
    {
        return new UpgradeData (data._upgradingMarkers, data._upgradeValue * multiplier, data._upgradeMultiplier);
    }
}
