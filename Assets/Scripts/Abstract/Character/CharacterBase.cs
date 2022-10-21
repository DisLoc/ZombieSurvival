using UnityEngine;

public abstract class CharacterBase : DamageableObject
{
    /// <summary>
    /// Object stats
    /// </summary>
    public abstract CharacterStats Stats { get; }

    public override int HP => (int)Stats.Health.Value;
    public override int MaxHP => Stats.Health.MaxHP;

    /// <summary>
    /// Moving with current speed in direction
    /// </summary>
    /// <param name="direction">Normalized vector</param>
    public abstract void Move(Vector3 direction);

    /// <summary>
    /// Character attacks with all Weapons it has
    /// </summary>
    public abstract void Attack();

    public override void TakeDamage(int damage)
    {
        Stats.Health.TakeDamage(damage);

        base.TakeDamage(damage);
    }

    public override void Die()
    {
        base.Die();
    }

    /// <summary>
    /// Upgrade character stats
    /// </summary>
    /// <param name="upgrade"></param>
    public virtual void GetUpgrade(Upgrade upgrade)
    {
        Stats.GetUpgrade(upgrade);
    }
}
