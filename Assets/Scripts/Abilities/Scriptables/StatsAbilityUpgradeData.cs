using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Abilities/Stats ability upgrade data", fileName = "New stats ability upgrade data")]
public sealed class StatsAbilityUpgradeData : AbilityUpgradeData
{
    [SerializeField] private CurrentUpgrade _repeatingUpgrade;

    public CurrentUpgrade RepeatingUpgrade => _repeatingUpgrade;
}