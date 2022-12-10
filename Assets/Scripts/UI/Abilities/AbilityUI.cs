using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private Text _abilityName;
    [SerializeField] private Image _abilityIcon;
    [SerializeField] private RectTransform _abilityLevelParent;
    [SerializeField] private LevelUI _levelPrefab;
    [SerializeField] private Text _upgradeDescriptionText;
    [SerializeField] private GameObject _newAbilityLabel;

    [Space(5)]
    [SerializeField] private CombineAbilityUI _combineAbilityPrefab;
    [SerializeField] private Text _combineText;
    [SerializeField] private RectTransform _combineAbilitiesParent;

    private AbilityGiver _abilityGiver;
    private AbilityContainer _ability;
    private List<LevelUI> _levels;
    private List<CombineAbilityUI> _combines;

    public void Initialize(AbilityGiver abilityGiver = null)
    {
        _levels = new List<LevelUI>();
        _combines = new List<CombineAbilityUI>();

        _abilityGiver = abilityGiver;
    }

    /// <summary>
    /// Set ability description, icon and level
    /// </summary>
    /// <param name="ability"></param>
    public void SetAbility(AbilityContainer ability)
    {
        if (ability == null)
        {
            if (_isDebug) Debug.Log("Missing ability!");

            return;
        }

        _ability = ability;

        _abilityName.text = _ability.Name;
        _abilityIcon.sprite = _ability.Icon;

        if (_ability.Stats.Level.Value == 0)
        {
            _newAbilityLabel.SetActive(true);
        }
        else
        {
            _newAbilityLabel.SetActive(false);
        }

        if (_levels.Count > 0)
        {
            foreach(LevelUI level in _levels)
            {
                Destroy(level.gameObject);
            }

            _levels.Clear();
        }

        for (int i = 0; i < (int)_ability.Stats.Level.MaxValue; i++)
        {
            LevelUI lvl = Instantiate(_levelPrefab, _abilityLevelParent);

            LevelUI.LevelType type = i < (int)ability.Stats.Level.Value ? LevelUI.LevelType.Unlocked : 
                                     i > (int)ability.Stats.Level.Value ? LevelUI.LevelType.Locked : 
                                     LevelUI.LevelType.Current;

            lvl.Initialize(type);

            _levels.Add(lvl);
        }

        _upgradeDescriptionText.text = _ability.CurrentUpgrade.Description;


        if (ability as PassiveAbility != null && (ability as PassiveAbility).CombinedAbilities.Count > 0)
        {
            if (_combines.Count > 0)
            {
                foreach (CombineAbilityUI combine in _combines)
                {
                    Destroy(combine.gameObject);
                }

                _combines.Clear();
            }

            _combineText.gameObject.SetActive(true);
            _combineAbilitiesParent.gameObject.SetActive(true);

            if ((ability as PassiveAbility).CombinedAbilities == null || (ability as PassiveAbility).CombinedAbilities.Count == 0)
            {
                _combineText.gameObject.SetActive(true);
                _combineAbilitiesParent.gameObject.SetActive(true);

                return;
            }

            foreach (CombineAbility combineAbility in (ability as PassiveAbility).CombinedAbilities)
            {
                if (combineAbility.CombinedWeapon == null)
                {
                    if (_isDebug) Debug.Log("Missing combine!");

                    return;
                }

                CombineAbilityUI combine = Instantiate(_combineAbilityPrefab, _combineAbilitiesParent);

                if (combineAbility.CombinedWeapon.Icon != null)
                {
                    combine.Initialize(combineAbility.CombinedWeapon.Icon);
                }

                _combines.Add(combine);
            }
        }
        else
        {
            _combineText.gameObject.SetActive(false);
            _combineAbilitiesParent.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Invokes by button
    /// </summary>
    public void ChooseUpgrade()
    {
        _abilityGiver?.GetAbility(_ability);
    }
}
