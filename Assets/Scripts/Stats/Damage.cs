using UnityEngine;

[System.Serializable]
public class Damage : Stat
{
    [Header("CriticalDamage settings")]
    [SerializeField] protected CriticalDamage _criticalDamage;

    public override float Value
    {
        get
        {
            if (_criticalDamage.CritRate.IsStrike)
            {
                return _value * _criticalDamage.Value;
            }
            else return _value;
        }
    }

    /// <summary>
    /// Chance to deal critical damage
    /// </summary>
    public Chance CritRate => _criticalDamage.CritRate;
    /// <summary>
    /// Critical damage multiplier
    /// </summary>
    public CriticalDamage CriticalDamage => _criticalDamage;

    public override void Initialize()
    {
        base.Initialize();

        _criticalDamage.Initialize();
    }

    public Damage(StatData damageStatData, StatData critStatData, StatData chanceStatData,
                  UpgradeList damageUpgradeList = null, UpgradeList critUpgradeList = null, UpgradeList chanceUpgradeList = null,
                  bool isDebug = false) : base(damageStatData, damageUpgradeList, isDebug)
    {
        _criticalDamage = new CriticalDamage(critStatData, chanceStatData, critUpgradeList, chanceUpgradeList, isDebug);

        _value = (_statData.BaseValue + _upgrades.UpgradesValue) * _upgrades.UpgradesMultiplier;

        if (_value < _minValue) _value = _minValue;
        if (!_statData.MaxValueIsInfinite && _value > _maxValue) _value = _maxValue;
    }

    public override bool Upgrade(Upgrade upgrade)
    {
        _criticalDamage.Upgrade(upgrade);

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
