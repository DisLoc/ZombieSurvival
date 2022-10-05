using UnityEngine;

[System.Serializable]
public class Level : IStat
{
    [SerializeField] private int _baseValue;
    [SerializeField] private int _minValue;
    [SerializeField] private int _maxValue;
    [SerializeField] private bool _maxValueIsInfinite;

    private int _level;

    public float BaseValue => _baseValue;
    public float Value => _level;
    public float MinValue => _minValue;
    public float MaxValue => _maxValue;
    public bool MaxValueIsInfinite => _maxValueIsInfinite;

    public void SetValue(float value = 1)
    {
        _level = (int)value;

        if (!_maxValueIsInfinite && _level > _maxValue)
            _level = _maxValue;

        if (_level < _minValue)
            _level = _minValue;
    }

    public void LevelUp()
    {
        if (_maxValueIsInfinite || _level < _maxValue)
            _level++;
        else return;
    }
}
