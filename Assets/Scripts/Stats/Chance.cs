using UnityEngine;

[System.Serializable]
public class Chance : Stat
{
    public float Probability => _value / _maxValue;

    public Chance(StatData statData, UpgradeList upgradeList = null, bool isDebug = false) : base(statData, upgradeList, isDebug) 
    {
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

    public bool IsStrike => Random.Range(_minValue, _maxValue) <= _value;
}
