using UnityEngine;

[System.Serializable]
public class AbilityStats : IObjectStats
{
    [SerializeField] protected UpgradeMarker _abilityMarker;
    [SerializeField] protected Level _level;

    public UpgradeMarker AbilityMarker => _abilityMarker;
    public Level Level => _level;

    public virtual void Initialize()
    {
        _level.SetValue(1);
    }

    public virtual void GetUpgrade(Upgrade upgrade) 
    {

    }
}
