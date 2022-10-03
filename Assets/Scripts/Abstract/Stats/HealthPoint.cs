using UnityEngine;

public abstract class HealthPoint : Stat
{
    public void TakeDamage(int damage)
    {
        _value -= damage;
    }

    public override bool Upgrade(Upgrade upgrade)
    {
        if (base.Upgrade(upgrade))
        {

            return true;
        }
        else return false;
    }

    protected override void CalculateValue()
    {
        throw new System.NotImplementedException();
    }
}
