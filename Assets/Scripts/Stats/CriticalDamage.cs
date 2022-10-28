using UnityEngine;

[System.Serializable]
public class CriticalDamage : Stat
{
    [Header("Chance settings")]
    [SerializeField] protected Chance _chance;

    public Chance CritRate => _chance;

    public override void Initialize()
    {
        base.Initialize();

        _chance.Initialize();
    }

    public CriticalDamage(StatData critStatData, StatData chanceStatData, 
                          UpgradeList critUpgradeList = null, UpgradeList chanceUpgradeList = null, 
                          bool isDebug = false) : base(critStatData, critUpgradeList, isDebug) 
    {
        _chance = new Chance(chanceStatData, chanceUpgradeList, isDebug);

        _value = (_statData.BaseValue + _upgrades.UpgradesValue) * _upgrades.UpgradesMultiplier;

        if (_value < _minValue) _value = _minValue;
        if (!_statData.MaxValueIsInfinite && _value > _maxValue) _value = _maxValue;
    }

    public override bool Upgrade(Upgrade upgrade)
    {
        _chance.Upgrade(upgrade);

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
