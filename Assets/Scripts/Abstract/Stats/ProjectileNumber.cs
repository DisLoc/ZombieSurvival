using UnityEngine;

public abstract class ProjectileNumber : Stat
{
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
