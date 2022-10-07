using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Abilities/Passive ability data")]
public class PassiveAbilityData : ScriptableObject
{
    [SerializeField] private PassiveAbility _passiveAbility;

    public PassiveAbility PassiveAbility => _passiveAbility;
}
