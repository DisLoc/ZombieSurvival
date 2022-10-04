using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : ScriptableObject
{
    [SerializeField] protected StatMarker _upgradingStatMarker;
    [SerializeField] protected float _upgradeValue = 0;
    [SerializeField] protected float _upgradeMultiplier = 1;

    public float UpgradeValue => _upgradeValue;
    public float UpgradeMultiplier => _upgradeMultiplier;

    public virtual string GetUpgradeMarker()
    {
        return _upgradingStatMarker.name;
    }

    public virtual List<string> GetUpgradeTypes()
    {
        List<string> types = new List<string>();
        types.Add(_upgradingStatMarker.name);

        return types;
    }
}
