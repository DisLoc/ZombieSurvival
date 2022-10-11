[System.Serializable]
public class HealthPoint : Stat
{
    public override void Initialize()
    {
        base.Initialize();

        _maxValue = _value;
    }

    public void TakeDamage(int damage)
    {
        _value -= damage;

        if (_value < _minValue) _value = _minValue;
        if (_value > _maxValue) _value = _maxValue;
    }

    public override bool Upgrade(Upgrade upgrade)
    {
        if (base.Upgrade(upgrade))
        {
            float percent = _value / _maxValue;

            _maxValue = (_statData.BaseValue + _upgrades.UpgradesValue) * _upgrades.UpgradesMultiplier;

            _value = _maxValue * percent;

            return true;
        }
        else return false;
    }
}
