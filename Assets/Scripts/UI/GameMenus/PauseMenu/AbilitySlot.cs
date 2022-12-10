using UnityEngine;
using UnityEngine.UI;

public class AbilitySlot : MonoBehaviour
{
    [SerializeField] private Image _abilityIcon;

    public Image AbilityIcon => _abilityIcon;
    
    public void Initialize(AbilityContainer ability)
    {
        _abilityIcon.sprite = ability.Icon;
    }
}
