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
    [SerializeField] private LevelUI _levelPrefab;
    [SerializeField] private Text _upgradeDescriptionText;

    [Inject] private AbilityGiver _abilityGiver;

    private Ability _ability;

    public void Initialize(Ability ability)
    {
        if (ability == null)
        {
            if (_isDebug) Debug.Log("Missing ability!");

            return;
        }

        _ability = ability;

        _abilityIcon.sprite = _ability.UpgradeIcon;

        for (int i = 0; i < (int)_ability.Stats.Level.MaxValue; i++)
        {
            LevelUI lvl = Instantiate(_levelPrefab, _abilityLevelParent);

            LevelUI.LevelType type = i <= (int)ability.Stats.Level.Value ? LevelUI.LevelType.Unlocked : 
                                     i > (int)ability.Stats.Level.Value + 1 ? LevelUI.LevelType.Locked : 
                                     LevelUI.LevelType.Current;

            lvl.Initialize(type);
        }

        _upgradeDescriptionText.text = _ability.CurrentUpgrade.Description;
    }

    public void ChooseUpgrade()
    {
        _abilityGiver.GetAbility(_ability);
    }
}
