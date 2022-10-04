using UnityEngine;

[System.Serializable]
public class Cooldown : Stat
{
    public override bool Upgrade(Upgrade upgrade)
    {
        if (base.Upgrade(upgrade))
        {


            return true;
        }
        else return false;
    }
}
