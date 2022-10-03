using System.Collections.Generic;
using UnityEngine;

public abstract class Stat : ScriptableObject, IStat, IUpgradeable
{
    [SerializeField] protected float _baseValue;
    [SerializeField] protected Level _level;

    protected float _value;
    protected List<Upgrade> _upgrades;

    public float BaseValue => _baseValue;
    public float Value => _value;
    public Level Level => _level;
    public List<Upgrade> Upgrades => _upgrades;

    public virtual void Initialize()
    {
        _upgrades = new List<Upgrade>();
        _value = _baseValue;
        _level.SetValue();
    }

    public virtual bool Upgrade(Upgrade upgrade)
    {
        return GetType().IsAssignableFrom(upgrade.GetUpgradeType());
    }
}
