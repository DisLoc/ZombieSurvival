using UnityEngine;

[System.Serializable]
public class ExpLevel
{
    [SerializeField] private int _maxLevel;
    [SerializeField] private int _expForLevel;
    [SerializeField] private float _levelExpMultipler;

    private int _minLevelExp = 0;
    private Expirience _exp;
    private int _level;

    public int Exp => _exp.Value;
    public int Level => _level;
    public float LevelProgress => (Exp - _minLevelExp) / ((_level - 1) * _levelExpMultipler * _expForLevel * _level - _minLevelExp);

    public void Add(int exp)
    {
        _exp.Add(exp);

        if (Exp >= (_level - 1) * _levelExpMultipler * _expForLevel * _level)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        _minLevelExp = (int)((_level - 1) * _levelExpMultipler * _expForLevel * _level);

        _level++;
    }
}
