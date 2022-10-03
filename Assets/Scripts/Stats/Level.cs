using UnityEngine;

[System.Serializable]
public class Level : IStat
{
    [SerializeField] private int _baseValue;
    [Tooltip("Set -1 for infinite level")]
    [SerializeField][Range(-1, 1000)] private int _maxValue;
    private int _level;

    public float BaseValue => _baseValue;
    public float Value => _level;

    public void SetValue(int value = 1)
    {
        _level = value;

        if (_level > _maxValue && _maxValue != -1) 
            _level = _maxValue;
    }

    public void LevelUp()
    {
        if (_level < _maxValue && _maxValue != -1)
            _level++;
        else return;
    }

    public void SetMaxLevel(int value)
    {
        _maxValue = value;
    }
}
