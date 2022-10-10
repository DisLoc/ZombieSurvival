using UnityEngine;

public class CriticalDamage : Stat
{
    [Header("Chance settings")]
    [SerializeField] protected Chance _chance;

    public float CritRate => _chance.Value;

    public override void Initialize()
    {
        base.Initialize();

        _chance.Initialize();
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
