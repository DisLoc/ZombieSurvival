using System.Collections.Generic;
using UnityEngine;

public abstract class Stat : ScriptableObject, IStat, IUpgradeable
{
    [SerializeField] protected float _baseValue;
    [Tooltip("Set -1 for infinite value")]
    [SerializeField] protected float _maxValue;
    [SerializeField] protected Level _level;

    protected float _value;
    protected List<Upgrade> _upgrades;

    public float BaseValue => _baseValue;
    public float Value => _value;
    public float MaxValue => _maxValue;
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
        if (upgrade.GetUpgradeType().IsAssignableFrom(GetType()))
        {
            _upgrades.Add(upgrade);

            return true;
        }
        else return false;
    }

    public void SetValue(float value)
    {
        _value = value;
    }

    public void SetMaxValue(float value)
    {
        _maxValue = value;
    }

    protected abstract void CalculateValue();
}
