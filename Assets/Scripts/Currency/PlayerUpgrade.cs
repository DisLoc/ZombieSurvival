using UnityEngine;

[System.Serializable]
public struct PlayerUpgrade
{
    [SerializeField] private int _requiredLevel;
    [SerializeField] private UpgradeData _damageData;
    [SerializeField] private UpgradeData _healthData;

    public int RequiredLevel => _requiredLevel;
    public UpgradeData DamageData => _damageData;
    public UpgradeData HealthData => _healthData;
}
