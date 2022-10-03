using UnityEngine;

[System.Serializable]
public class Expirience : IStat
{
    [SerializeField] private int _baseValue;
    private int _expirience;

    public float BaseValue => _baseValue;
    public float Value => _expirience;

    public void SetValue(int value = 0)
    {
        _expirience = value;
    }

    public void Add(int exp)
    {
        _expirience += exp;
    }
}
