using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : ScriptableObject
{
    [SerializeField] protected UpgradeMarker _upgradingStatMarker;
    [SerializeField] protected float _upgradeValue = 0;
    [SerializeField][Range(0, 10)] protected float _upgradeMultiplier = 1;

    public float UpgradeValue => _upgradeValue;
    public float UpgradeMultiplier => _upgradeMultiplier;

    public virtual string GetUpgradeMarker()
    {
        return _upgradingStatMarker.name;
    }

    public virtual List<string> GetUpgradeMarkers()
    {
        List<string> types = new List<string>();
        types.Add(_upgradingStatMarker.name);

        return types;
    }
}
