using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : CharacterStats
{
    [SerializeField] protected int plusState;
    /*
    public virtual void PlusMaxHP()
    {
        _maxHP += plusState;
    }

    public virtual void PlusBaseDamage()
    {
        _baseDamage += plusState;
    }

    public virtual void PlusAttackRange()
    {
        _attackRange += plusState;
    }

    public virtual void PlusAttackCoolDown()
    {
        _attackCooldown += plusState;
    }

    public virtual void PlusVelocity()
    {
        _velocity += plusState;
    }*/
}
