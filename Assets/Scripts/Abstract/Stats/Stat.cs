using UnityEngine;

[System.Serializable]
public abstract class Stat : IStat, IUpgradeable
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    [Header("Stat settings")]
    [SerializeField] protected StatData _statData;

    protected float _value;
    protected float _maxValue;
    protected UpgradeList _upgrades;

    public float BaseValue => _statData.BaseValue;
    public float Value => _value;
    public float MaxValue => _statData.MaxValue;
    public Level Level => _statData.Level;
    public UpgradeList Upgrades => _upgrades;

    public virtual void Initialize()
    {
        _upgrades = new UpgradeList();
        _value = _statData.BaseValue;
    }

    public virtual bool Upgrade(Upgrade upgrade)
    {
        if (_statData.Marker.name.Equals(upgrade.GetUpgradeMarker()))
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
}
