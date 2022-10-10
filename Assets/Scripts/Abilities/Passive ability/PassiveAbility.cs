using UnityEngine;

[System.Serializable]
public sealed class PassiveAbility : Ability
{
    [SerializeField] private AbilityStats _stats;
    [SerializeField] private CurrentUpgrade _repeatingUpgrade;

    public override AbilityStats Stats => _stats;
    public override CurrentUpgrade CurrentUpgrade => _repeatingUpgrade;
}