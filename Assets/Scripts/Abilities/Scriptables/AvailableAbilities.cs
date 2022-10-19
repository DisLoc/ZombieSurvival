using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Abilities/Available abilities", fileName = "New abilities list", order = 0)]
public sealed class AvailableAbilities : ScriptableObject
{
    [SerializeField] private List<PassiveAbility> _passiveAbilities;
    [SerializeField] private List<Weapon> _activeAbilities;

    public List<AbilityContainer> Abilities
    {
        get
        {
            List<AbilityContainer> abilities = new List<AbilityContainer>(_activeAbilities.Count + _passiveAbilities.Count);

            abilities.AddRange(_passiveAbilities);
            abilities.AddRange(_activeAbilities);

            return abilities;
        }
    }
}
