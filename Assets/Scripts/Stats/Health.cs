using UnityEngine;

[System.Serializable]
public class Health : Stat
{
    [SerializeField] private MaxHP _maxHP;

    /// <summary>
    /// Means MaxValue of this stat
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

    public Health(StatData hpStatData, StatData maxHPStatData, UpgradeList maxHPUpgradeList = null, UpgradeList hpUpgradeList = null, 
                  bool isDebug = false) : base(hpStatData, hpUpgradeList, isDebug) 
    {
        _maxHP = new MaxHP(maxHPStatData, maxHPUpgradeList, isDebug);

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

    public void TakeDamage(float damage)
    {
        _value -= damage;

        if (_value < _minValue) _value = _minValue;
        if (_value > _maxHP.Value) _value = _maxHP.Value;
    }

    public void Heal(float heal)
    {
        _value += heal;

        if (_value < _minValue) _value = _minValue;
        if (_value > _maxHP.Value) _value = _maxHP.Value;
    }

    /// <summary>
    /// Upgrades dispel after updating value (heal/take damage). Also upgrade MaxHP.
    /// </summary>
    /// <param name="upgrade">If use upgrades for healing/taking damage: UpgradeMultiplier = percent of MaxHP heal/take damage</param>
    /// <returns></returns>
    public override bool Upgrade(Upgrade upgrade)
    {
        float currentPercent = _value / _maxHP.Value;

        bool isMaxUpgrade = _maxHP.Upgrade(upgrade);
        bool isUpgade = base.Upgrade(upgrade);
        
        if (isUpgade)
        {
            _value += _upgrades.UpgradesValue + _upgrades.UpgradesMultiplier * _maxHP.Value;

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

        if (_value < _minValue)
        {
            _value = _minValue;
        }

        return isMaxUpgrade || isUpgade;
    }
}
