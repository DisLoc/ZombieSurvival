using UnityEngine;

public abstract class MoveSpeed : Stat
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
