using UnityEngine;

[System.Serializable]
public class PassiveAbility : Ability
{
    [SerializeField] private CurrentUpgrade _repeatingUpgrade;

    public override CurrentUpgrade CurrentUpgrade => _repeatingUpgrade;
}