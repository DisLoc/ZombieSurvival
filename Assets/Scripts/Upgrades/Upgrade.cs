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
}
