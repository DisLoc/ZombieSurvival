using UnityEngine;

[System.Serializable]
public class Expirience : IStat
{
    [SerializeField] private int _baseValue;
    [SerializeField] private float _minValue;
    [SerializeField] private int _maxValue;
    [SerializeField] private bool _maxValueIsInfinite;

    private float _expirience;

    public float BaseValue => _baseValue;
    public float Value => _expirience;
    public float MinValue => _minValue;
    public float MaxValue => _maxValue;
    public bool MaxValueIsInfinite => _maxValueIsInfinite;

    public void SetValue(float value = 0)
    {
        _expirience = value;

        if (!_maxValueIsInfinite && _expirience > _maxValue)
            _expirience = _maxValue; 

        if (_expirience < _minValue)
            _expirience = _minValue;
    }

    public void Add(float exp)
    {
        _expirience += exp;
    }
}
