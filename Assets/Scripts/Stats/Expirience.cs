using UnityEngine;

public struct Expirience
{
    private int _expirience;
    public int Value => _expirience;

    public Expirience(int exp = 0)
    {
        _expirience = exp;
    }

    public void Add(int exp)
    {
        _expirience += exp;
    }
}
