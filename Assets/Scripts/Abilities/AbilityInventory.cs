using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityInventory<T> where T : MonoBehaviour
{
    public List<T> activeAbilities = new List<T>(5);
    public List<T> passiveAbilities = new List<T>(5);

    public List<int> activeAbilityLevel = new List<int>(5);
    public List<int> passiveAbilityLevel = new List<int>(5);


    public void AddActiveAbility(T ability)
    {
        activeAbilities.Add(ability);
    }

    public void AddPassiveAbility(T ability)
    {
        passiveAbilities.Add(ability);
    }
}
