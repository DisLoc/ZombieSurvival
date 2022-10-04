using UnityEngine;

[System.Serializable]
public class Expirience : IStat
{
    [SerializeField] private int _baseValue;
    [Tooltip("Set -1 for infinite level")]
    [SerializeField] private int _maxValue;
    private float _expirience;

    public float BaseValue => _baseValue;
    public float Value => _expirience;
    public float MaxValue => _maxValue;

    public void SetValue(float value = 0)
    {
        _expirience = value;

        if (_expirience > _maxValue && _maxValue != -1)
        {
            _expirience = _maxValue;
        }
    }

    public void SetMaxValue(float value)
    {
        _maxValue = (int)value;
    }

    public void Add(float exp)
    {
        _expirience += exp;
    }
}
