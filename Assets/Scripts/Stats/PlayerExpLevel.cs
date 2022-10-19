using UnityEngine;

[System.Serializable]
public class PlayerExpLevel : Stat
{
    [SerializeField] protected Expirience _exp;
    [SerializeField] protected ExpBar _expBar;

    [Header("LevelUp settings")]
    [SerializeField] protected int _baseExpForLevelUp;
    [Tooltip("ExpForLevel = BaseExpForLevel * LevelMultiplier * CurrentLevel")]
    [SerializeField] protected float _levelMultiplier;

    public Expirience Exp => _exp;
    public int ExpForLevel => (int)(_baseExpForLevelUp * _levelMultiplier * _value);
    public float LevelProgress => _exp.Value / ExpForLevel;

    public override void Initialize()
    {
        base.Initialize();

        _exp.SetValue(0);

        _expBar.Initialize(this);
    }

    public void AddExp(int exp)
    {
        if (_isDebug) Debug.Log("Add " + exp + " expirience. Total: " + (exp + _upgrades.UpgradesValue) * _upgrades.UpgradesMultiplier);
             
        _exp.Add((exp + _upgrades.UpgradesValue) * _upgrades.UpgradesMultiplier);

        if (LevelProgress >= 1)
        {
            _exp.SetValue(0);
            _value++;

            if (_expBar != null)
            {
                _expBar.UpdateLevel();
            }
            else if (_isDebug) Debug.Log("Missing ExpBar!");

            EventBus.Publish<IPlayerLevelUp>(handler => handler.OnPlayerLevelUp());
        }

        if (_expBar != null)
        {
            _expBar.UpdateExp();
        }
        else if (_isDebug) Debug.Log("Missing ExpBar!");
    }
}
