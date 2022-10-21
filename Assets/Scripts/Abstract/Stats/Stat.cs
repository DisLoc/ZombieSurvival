using UnityEngine;

[System.Serializable]
public abstract class Stat : IStat, IUpgradeable
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    [Header("Stat settings")]
    [SerializeField] protected StatData _statData;

    protected float _value;
    protected float _minValue;
    protected float _maxValue;
    protected UpgradeList _upgrades;

    public float BaseValue => _statData.BaseValue;
    /// <summary>
    /// Current value of stat
    /// </summary>
    public float Value => _value;
    public float MinValue => _minValue;
    public float MaxValue => _maxValue;
    public bool MaxValueIsInfinite => _statData.MaxValueIsInfinite;
    /// <summary>
    /// Upgrades this stat getted
    /// </summary>
    public UpgradeList Upgrades => _upgrades;

    /// <summary>
    /// Initialize stat based on StatData
    /// </summary>
    public virtual void Initialize()
    {
        _upgrades = new UpgradeList();

        _value = _statData.BaseValue;
        _minValue = _statData.MinValue;
        _maxValue = _statData.MaxValue;

        if (_isDebug) Debug.Log(_statData.name + " initialized");
    }

    public virtual bool Upgrade(Upgrade upgrade)
    {
        if (_isDebug) Debug.Log(_statData.name + " try upgrade: " + upgrade.name);

        int upgrades = 0;

        foreach(UpgradeData data in upgrade.Upgrades)
        {
            if (data.UpgradingStatMarker.Equals(_statData.Marker))
            {
                upgrades++;

                _upgrades.Add(data);
            }
        }

        if (_statData.MaxValueIsInfinite)
        {
            _maxValue = (_statData.MaxValue + _upgrades.UpgradesValue) * _upgrades.UpgradesMultiplier;
        }

        return upgrades > 0;
    }

    /// <summary>
    /// Set current value. Cant be less than MinValue and more than MaxValue (if MaxValue is not infinite)
    /// </summary>
    /// <param name="value">Value need to set</param>
    public void SetValue(float value)
    {
        _value = value;

        if (_isDebug) Debug.Log(_statData.name + " set value: " + _value);

        if (!_statData.MaxValueIsInfinite && _value > _statData.MaxValue)
        {
            if (_isDebug) Debug.Log(_statData.name + " set max value: " + _value);

            _value = _statData.MaxValue;
        }

        if (_value < _statData.MinValue)
        {
            if (_isDebug) Debug.Log(_statData.name + " set min value: " + _value);

            _value = _statData.MinValue;
        }
    }
}
