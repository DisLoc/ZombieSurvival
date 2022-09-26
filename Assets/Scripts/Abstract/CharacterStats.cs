using UnityEngine;

[System.Serializable]
public class CharacterStats : MonoBehaviour
{
    [SerializeField] protected HealthPoint _healthPoints;
    [SerializeField] protected Damage _baseDamage;
    [SerializeField] protected AttackRange _attackRange;
    [SerializeField] protected Cooldown _attackCooldown;
    [SerializeField] protected MoveSpeed _velocity;
    [SerializeField] protected Weapon _weapon;

    public HealthPoint HP => _healthPoints;
    public float BaseDamage => _baseDamage.Value;
    public float AttackRange => _attackRange.Value;
    public float AttackCooldown => _attackCooldown.Value;
    public float Velocity => _velocity.Value;

    public virtual void Initialize()
    {
        _healthPoints.Initialize();
    }
}
