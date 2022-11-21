using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

[CreateAssetMenu(menuName = "ZombieSurvival/Upgrades/Upgrade", fileName = "New upgrade")]
public class Upgrade : ScriptableObject
{
    [SerializeField] private bool _isAbilityUpgrade;
    [SerializeField] private AbilityMarker _abilityMarker;

    [SerializeField] private List<UpgradeData> _upgrades;

    public bool IsAbilityUpgrade => _isAbilityUpgrade;
    public AbilityMarker AbilityMarker => _abilityMarker;
    public List<UpgradeData> Upgrades => _upgrades;

    public Upgrade (List<UpgradeData> data, AbilityMarker abilityMarker = null, bool isAbilityUpgrade = false)
    {
        _upgrades = data;
        _abilityMarker = abilityMarker;
        _isAbilityUpgrade = isAbilityUpgrade;
    }

    public Upgrade (UpgradeData data, AbilityMarker abilityMarker = null, bool isAbilityUpgrade = false)
    {
        _upgrades = new List<UpgradeData>();
        _upgrades.Add(data);

        _abilityMarker = abilityMarker;
        _isAbilityUpgrade = isAbilityUpgrade;
    }

    public static Upgrade operator +(Upgrade first, Upgrade other)
    {
        List<UpgradeData> data = new List<UpgradeData> ();

        data.AddRange(first.Upgrades);
        data.AddRange(other.Upgrades);

        return new Upgrade(data);
    }

    public static Upgrade operator +(Upgrade upgrade, UpgradeData data)
    {
        List<UpgradeData> datas = new List<UpgradeData>();

        datas.AddRange(upgrade.Upgrades);
        if (data != null)
        {
            datas.Add(data);
        }

        return new Upgrade(data);
    }
}
