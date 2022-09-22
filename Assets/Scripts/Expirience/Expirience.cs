using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Expirience
{
    [SerializeField] private int _exp;
    public int Value => _exp;

    public Expirience(int exp = 0)
    {
        _exp = exp;
    }

    public void Add(int exp) => _exp += exp;
}
