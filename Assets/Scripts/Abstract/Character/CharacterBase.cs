using UnityEngine;

public abstract class CharacterBase : DamageableObject
{
    public abstract CharacterStats Stats { get; }

    public override int HP => (int)Stats.HP.Value;
    public override int MaxHP => (int)Stats.HP.MaxValue;

    public abstract void Move(Vector3 direction);

    public abstract void Attack();

    public override void TakeDamage(int damage)
    {
        Stats.HP.TakeDamage(damage);

        base.TakeDamage(damage);
    }

    public override void Die()
    {
        base.Die();
    }

    public virtual void GetUpgrade(Upgrade upgrade)
    {

    }
}
