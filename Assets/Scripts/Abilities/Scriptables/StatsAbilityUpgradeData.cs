using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Abilities/Stats ability upgrade data", fileName = "New stats ability upgrade data")]
public sealed class StatsAbilityUpgradeData : AbilityUpgradeData
{
    [Tooltip("This upgrade will be repeat until ability reaches its max level")]
    [SerializeField] private CurrentUpgrade _repeatingUpgrade;

    public CurrentUpgrade RepeatingUpgrade => _repeatingUpgrade;
}