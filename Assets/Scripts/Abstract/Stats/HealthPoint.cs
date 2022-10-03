using UnityEngine;

public abstract class HealthPoint : Stat
{
    [SerializeField] protected int _maxHP;

    public int MaxHP => _maxHP;

    public void TakeDamage(int damage)
    {
        _value -= damage;
    }
}
