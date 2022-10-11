using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AuraAbility : WeaponAbility
{
    [SerializeField] protected WeaponAbilityStats _stats;

    public override AbilityStats Stats => _stats;

    public override void Initialize()
    {
        base.Initialize();
    }

    public override bool Upgrade(Upgrade upgrade)
    {
        if (base.Upgrade(upgrade))
        {
            _stats.GetUpgrade(upgrade);

            return true;
        }
        else return false;
    }
}
