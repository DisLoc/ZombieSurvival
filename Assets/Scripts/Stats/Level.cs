using UnityEngine;

[System.Serializable]
public class Level : IStat
{
    [SerializeField] private int _baseValue;
    [Tooltip("Set -1 for infinite level")]
    [SerializeField] private int _maxValue;
    private int _level;

    public float BaseValue => _baseValue;
    public float Value => _level;
    public float MaxValue => _maxValue;

    public void SetValue(float value = 1)
    {
        _level = (int)value;

        if (_level > _maxValue && _maxValue != -1) 
            _level = _maxValue;
    }

    public void LevelUp()
    {
        if (_level < _maxValue && _maxValue != -1)
            _level++;
        else return;
    }

    public void SetMaxValue(float value)
    {
        _maxValue = (int)value;
    }
}
