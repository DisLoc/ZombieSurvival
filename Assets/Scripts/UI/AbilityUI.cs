using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AbilityUI : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private Image _abilityIcon;
    [SerializeField] private RectTransform _abilityLevelParent;
    [SerializeField] private Image _levelPrefab;
    [SerializeField] private Text _upgradeDescriptionText;

    [Inject] private AbilityGiver _abilityGiver;

    private Ability _ability;

    public void Initialize(Ability ability)
    {
        _ability = ability;

        _abilityIcon.sprite = ability.Icon;

        for (int i = 0; i < (int)ability.Level.MaxValue; i++)
        {
            Instantiate(_levelPrefab, _abilityLevelParent);
        }

        _upgradeDescriptionText.text = ability.CurrentUpgrade.Description;
    }

    public void ChooseUpgrade()
    {
        _abilityGiver.GetAbility(_ability);
    }
}
