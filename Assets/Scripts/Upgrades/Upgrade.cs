using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Upgrades/Upgrade", fileName = "New upgrade")]
public class Upgrade : ScriptableObject
{
    [SerializeField] private bool _isAbilityUpgrade;
    [SerializeField] private UpgradeMarker _abilityMarker;

    [SerializeField] private List<UpgradeData> _upgrades;

    public bool IsAbilityUpgrade => _isAbilityUpgrade;
    public UpgradeMarker AbilityMarker => _abilityMarker;
    public List<UpgradeData> Upgrades => _upgrades;

    private Upgrade (List<UpgradeData> data, UpgradeMarker abilityMarker = null, bool isAbilityUpgrade = false)
    {
        _upgrades = data;
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
}
