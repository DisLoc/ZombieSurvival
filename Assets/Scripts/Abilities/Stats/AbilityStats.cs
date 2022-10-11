using UnityEngine;

[System.Serializable]
public class AbilityStats : IObjectStats
{
    [SerializeField] protected UpgradeMarker _abilityMarker;
    [SerializeField] protected Level _level;

    public Level Level => _level;

    public virtual void Initialize()
    {
        _level.SetValue();
    }

    public virtual void GetUpgrade(Upgrade upgrade) 
    {
        if (upgrade.IsAbilityUpgrade && upgrade.AbilityMarker.Equals(_abilityMarker))
        {
            _level.LevelUp();
        }
    }
}
