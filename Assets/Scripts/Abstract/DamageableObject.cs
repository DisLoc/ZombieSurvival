using UnityEngine;

public abstract class DamageableObject : MonoBehaviour, IDamageable
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    [Header("Health settings")]
    [Tooltip("If object is immortal it takes damage but cant die")]
    [SerializeField] protected bool _isImmortal;
    [Tooltip("Field can be null if needed")]
    [SerializeField] protected HPBar _healthBar;

    public bool IsImmortal => _isImmortal;
    /// <summary>
    /// Current health points of object
    /// </summary>
    public abstract int HP { get; }
    /// <summary>
    /// Max health points of object
    /// </summary>
    public abstract int MaxHP { get; }

    public virtual void TakeDamage(int damage)
    {
        if (_isDebug) Debug.Log(name + " take " + damage + " damage");

        _healthBar?.UpdateHealth();

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
