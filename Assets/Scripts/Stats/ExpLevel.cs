using UnityEngine;

[System.Serializable]
public class ExpLevel : Stat
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

    public void AddExp(int exp)
    {
        _exp.Add((exp + _upgrades.UpgradesValue) * _upgrades.UpgradesMultiplier);

        if (LevelProgress >= 1)
        {
            _exp.SetValue(0);
            _value++;

            if (_expBar != null)
            {
                _expBar.UpdateExp();
            }
            else if (_isDebug) Debug.Log("Missing ExpBar!");
        }
    }
}
