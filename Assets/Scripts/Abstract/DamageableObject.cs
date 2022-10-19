using UnityEngine;

public abstract class DamageableObject : MonoBehaviour, IDamageable
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    [Header("Settings")]
    [SerializeField] protected HPBar _healthBar;
    [SerializeField] protected bool _isImmortal;

    public bool IsImmortal => _isImmortal;
    public abstract int HP { get; }
    public abstract int MaxHP { get; }

    public virtual void TakeDamage(int damage)
    {
        if (_isDebug) Debug.Log(name + " take " + damage + " damage");

        _healthBar.UpdateHealth();

        if (HP <= 0 && !_isImmortal)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        if (_isDebug) Debug.Log(name + " dies");
    }
}
