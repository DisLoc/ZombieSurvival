using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Abilities/Stats ability data", fileName = "New stats ability data")]
public sealed class StatsAbilityData : AbilityData
{
    [SerializeField] private AbilityStats _stats;
    [SerializeField] private CurrentUpgrade _repeatingUpgrade;

    public override AbilityStats Stats => _stats;
    public CurrentUpgrade RepeatingUpgrade => _repeatingUpgrade;
}