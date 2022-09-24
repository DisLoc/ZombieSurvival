using UnityEngine;

[System.Serializable]
public class CharacterStats
{
    [SerializeField] protected int _maxHP;
    [SerializeField] protected int _baseDamage;
    [SerializeField] protected int _attackRange;
    [SerializeField] protected int _attackCooldown;
    [SerializeField] protected int _velocity;

    [HideInInspector] public int HP = 0;
    public int MaxHP => _maxHP;
    public int BaseDamage => _baseDamage;
    public int AttackRange => _attackRange;
    public int AttackCooldown => _attackCooldown;
    public int Velocity => _velocity;


    public virtual void Initialize()
    {
        HP = _maxHP;
    }
}
