using UnityEngine;

[System.Serializable]
public class Duration : Stat
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