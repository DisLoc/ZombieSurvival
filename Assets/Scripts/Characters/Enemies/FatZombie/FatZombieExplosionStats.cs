using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FatZombieExplosionStats : IObjectStats
{
    [SerializeField] private Damage _explosionDamage;
    [SerializeField] private Duration _explosionDuration;
    [SerializeField] private Radius _explosionRadius;

    public Damage ExplosionDamage => _explosionDamage;
    public Duration ExplosionDuration => _explosionDuration;
    public Radius ExplosionRadius => _explosionRadius;

    public void Initialize()
    {
        _explosionDamage.Initialize();
        _explosionDuration.Initialize();
        _explosionRadius.Initialize();

    }
    public void GetUpgrade(Upgrade upgrade)
    {
        _explosionDamage.Upgrade(upgrade);
        _explosionDuration.Upgrade(upgrade);
        _explosionRadius.Upgrade(upgrade);
    }

    public void DispelUpgrade(Upgrade upgrade)
    {
        _explosionDamage.DispelUpgrade(upgrade);
        _explosionDuration.DispelUpgrade(upgrade);
        _explosionRadius.DispelUpgrade(upgrade);
    }

    public void DispelUpgrades(List<Upgrade> upgrades)
    {
        foreach(Upgrade upgrade in upgrades)
        {
            DispelUpgrade(upgrade);
        }
    }
}
