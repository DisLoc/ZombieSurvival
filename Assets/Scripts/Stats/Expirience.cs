using UnityEngine;

[System.Serializable]
public class Expirience : IStat
{
    [SerializeField] private ExpirienceData _data;

    private float _expirience;

    public float BaseValue => _data.BaseValue;
    public float Value => _expirience;
    public float MinValue => _data.MinValue;
    public float MaxValue => _data.MaxValue;
    public bool MaxValueIsInfinite => _data.MaxValueIsInfinite;

    public void Initialize()
    {
        _expirience = _data.BaseValue;
    }

    public Expirience(ExpirienceData data)
    {
        if (data != null)
        {
            _data = data;
            _expirience = _data.BaseValue;
        }
    }

    public void SetValue(float value = 0)
    {
        _expirience = value;

        if (!_data.MaxValueIsInfinite && _expirience > _data.MaxValue)
            _expirience = _data.MaxValue; 

        if (_expirience < _data.MinValue)
            _expirience = _data.MinValue;
    }

    public void Add(float exp)
    {
        _expirience += exp;

        if (!_data.MaxValueIsInfinite && _expirience > _data.MaxValue)
            _expirience = _data.MaxValue;

        if (_expirience < _data.MinValue)
            _expirience = _data.MinValue;
    }
}

[System.Serializable]
public class ExpirienceData
{
    [SerializeField] private int _baseValue;
    [SerializeField] private float _minValue;
    [SerializeField] private int _maxValue;
    [SerializeField] private bool _maxValueIsInfinite;

    public int BaseValue => _baseValue;
    public float MinValue => _minValue;
    public int MaxValue => _maxValue;
    public bool MaxValueIsInfinite => _maxValueIsInfinite;

    public ExpirienceData(int baseValue, int minValue, int maxValue, bool maxValueIsInfinite = false)
    {
        _baseValue = baseValue;
        _minValue = minValue;
        _maxValue = maxValue;
        _maxValueIsInfinite = maxValueIsInfinite;
    }
}