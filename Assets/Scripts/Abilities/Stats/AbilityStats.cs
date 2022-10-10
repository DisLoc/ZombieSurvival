using UnityEngine;

[System.Serializable]
public class AbilityStats : IObjectStats
{
    [SerializeField] protected UpgradeMarker _upgradeMarker;
    [SerializeField] protected Level _level;

    public Level Level => _level;

    public virtual void Initialize()
    {
        _level.SetValue();
    }

    public virtual void GetUpgrade(Upgrade upgrade) 
    {
        if (upgrade.IsAbilityUpgrade && upgrade.AbilityMarker.Equals(_upgradeMarker))
        {
            _level.LevelUp();
        }
    }
}
