using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : DamageableObject, IFixedUpdatable
{
    [Tooltip("Can be null if HealthBar is null")]
    [SerializeField] protected HPBarCanvas _hpCanvas;

    /// <summary>
    /// Object stats
    /// </summary>
    public abstract CharacterStats Stats { get; }

    public override int HP => (int)Stats.Health.Value;
    public override int MaxHP => Stats.Health.MaxHP;

    /// <summary>
    /// Heal character based on fixedDeltaTime and regeneration value
    /// </summary>
    public virtual void OnFixedUpdate()
    {
        Stats.Health.Heal(Stats.Regeneration.Value * Time.fixedDeltaTime);

        if (_healthBar != null) _healthBar.UpdateHealth();
        if (_hpCanvas != null) _hpCanvas.OnFixedUpdate();
    }

    /// <summary>
    /// Moving with current speed in direction
    /// </summary>
    /// <param name="direction">Normalized vector</param>
    public abstract void Move(Vector3 direction);

    /// <summary>
    /// Character attacks with all Weapons it has
    /// </summary>
    protected abstract void Attack();

    public override void TakeDamage(int damage)
    {
        Stats.Health.TakeDamage(damage);

        base.TakeDamage(damage);
    }

    /// <summary>
    /// Upgrade character stats
    /// </summary>
    /// <param name="upgrade"></param>
    public virtual void GetUpgrade(Upgrade upgrade)
    {
        Stats.GetUpgrade(upgrade);
    }

    /// <summary>
    /// Dispel upgrade on stats
    /// </summary>
    /// <param name="upgrade"></param>
    public virtual void DispelUpgrade(Upgrade upgrade)
    {
        Stats.DispelUpgrade(upgrade);
    }

    public virtual void DispelUpgrades(List<Upgrade> upgrades)
    {
        foreach(Upgrade upgrade in upgrades)
        {
            DispelUpgrade(upgrade);
        }
    }
}
