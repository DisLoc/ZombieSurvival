[System.Serializable]
public class AbilitiesPerLevel : Stat
{
    public override bool Upgrade(Upgrade upgrade)
    {
        if (base.Upgrade(upgrade))
        {
            _value = (_statData.BaseValue + _upgrades.UpgradesValue) * _upgrades.UpgradesMultiplier;

            if (_value < _minValue) _value = _minValue;
            if (_value > _maxValue) _value = _maxValue;

            return true;
        }
        else return false;
    }
}
