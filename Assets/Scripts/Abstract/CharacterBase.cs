using UnityEngine;

public abstract class CharacterBase : MonoBehaviour, IDamageable
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;
    [SerializeField] protected HPBar _healthBar;

    public abstract CharacterStats Stats { get; }

    public int HP => (int)Stats.HP.Value;
    public int MaxHP => (int)Stats.HP.MaxHP;

    public abstract void Move(Vector3 direction);

    public abstract void Attack();

    public virtual void TakeDamage(int damage)
    {
        if (_isDebug) Debug.Log(name + " take " + damage + " damage");

        Stats.HP.TakeDamage(damage);
        _healthBar.UpdateHealth(HP);

        if (HP <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        if (_isDebug) Debug.Log(name + " dies");
    }
}
