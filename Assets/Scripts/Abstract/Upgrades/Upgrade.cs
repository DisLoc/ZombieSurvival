using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : ScriptableObject
{
    [SerializeField] protected Stat _upgradingStatMarker;
    [SerializeField] protected float _upgradeValue;
    [SerializeField] protected float _upgradeMultiplier;

    public float UpgradeValue => _upgradeValue;
    public float UpgradeMultiplier => _upgradeMultiplier;

    public virtual Type GetUpgradeType()
    {
        return _upgradingStatMarker.GetType();
    }

    public virtual List<Type> GetUpgradeTypes()
    {
        List<Type> types = new List<Type>();
        types.Add(_upgradingStatMarker.GetType());

        return types;
    }
}
