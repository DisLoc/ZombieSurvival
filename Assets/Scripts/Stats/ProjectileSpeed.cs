using UnityEngine;

[System.Serializable]
public class ProjectileSpeed : Stat
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
