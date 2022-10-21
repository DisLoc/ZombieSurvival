using UnityEngine;

[System.Serializable]
public class Health : Stat
{
    [SerializeField] private MaxHP _maxHP;

    /// <summary>
    /// Override MaxValue of this stat
    /// </summary>
    public int MaxHP => (int)_maxHP.Value;

    public override void Initialize()
    {
        base.Initialize();

        _maxHP.Initialize();

        if (_value > _maxHP.Value)
        {
            if (_isDebug) Debug.Log("HP > MaxHP! Fixing...");

            _value = _maxHP.Value;
        }
        else if (_value < _maxHP.Value)
        {
            if (_isDebug) Debug.Log("HP < MaxHP ???");
        }

    }

    public void TakeDamage(int damage)
    {
        _value -= damage;

        if (_value < _minValue) _value = _minValue;
        if (_value > _maxHP.Value) _value = _maxHP.Value;
    }

    public void Heal(int heal)
    {
        _value += heal;

        if (_value < _minValue) _value = _minValue;
        if (_value > _maxHP.Value) _value = _maxHP.Value;
    }

    /// <summary>
    /// Upgrades dispel after updating value. Also upgrade MaxHP
    /// </summary>
    /// <param name="upgrade"></param>
    /// <returns></returns>
    public override bool Upgrade(Upgrade upgrade)
    {
        float currentPercent = _value / _maxHP.Value;

        bool isMaxUpgrade = _maxHP.Upgrade(upgrade);
        bool isUpgade = base.Upgrade(upgrade);
        
        if (isUpgade)
        {
            _value = (_statData.BaseValue + _upgrades.UpgradesValue) * _upgrades.UpgradesMultiplier;

            _upgrades.DispelAll();
        }

        if (isMaxUpgrade)
        {
            if (_value < _maxHP.Value * currentPercent)
            {
                _value = _maxHP.Value * currentPercent;
            }
        }

        if (_value > _maxHP.Value)
        {
            if (_isDebug) Debug.Log("HP > MaxHP! Fixing...");

            _value = _maxHP.Value;
        }

        return isMaxUpgrade || isUpgade;
    }
}
