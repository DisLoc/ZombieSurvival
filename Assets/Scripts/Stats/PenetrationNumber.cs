using UnityEngine;

[System.Serializable]
public class PenetrationNumber : Stat
{
    [SerializeField] protected bool _valueIsInfinite;

    public bool ValueIsInfinite => _valueIsInfinite;

    public PenetrationNumber(StatData statData, bool valueIsInfinite, UpgradeList upgradeList = null, bool isDebug = false) : base(statData, upgradeList, isDebug) 
    { 
        _valueIsInfinite = valueIsInfinite;

        _value = (_statData.BaseValue + _upgrades.UpgradesValue) * _upgrades.UpgradesMultiplier;

        if (_value < _minValue) _value = _minValue;
        if (!_statData.MaxValueIsInfinite && _value > _maxValue) _value = _maxValue;
    }

    public override bool Upgrade(Upgrade upgrade)
    {
        if (base.Upgrade(upgrade))
        {
            _value = (_statData.BaseValue + _upgrades.UpgradesValue) * _upgrades.UpgradesMultiplier;

            if (_value < _minValue) _value = _minValue;
            if (!_statData.MaxValueIsInfinite && _value > _maxValue) _value = _maxValue;

            return true;
        }
        else return false;
    }
}
