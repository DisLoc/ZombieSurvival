using UnityEngine;

[System.Serializable]
public class Damage : Stat
{
    [Header("CriticalDamage settings")]
    [SerializeField] protected CriticalDamage _criticalDamage;

    /// <summary>
    /// Chance to deal critical damage
    /// </summary>
    public float CritRate => _criticalDamage.CritRate;
    /// <summary>
    /// Critical damage multiplier
    /// </summary>
    public float CriticalDamage => _criticalDamage.Value;

    public override void Initialize()
    {
        base.Initialize();

        _criticalDamage.Initialize();
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
