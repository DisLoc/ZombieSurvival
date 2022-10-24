using System.Collections.Generic;
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

    [Space(5)]
    [SerializeField] private CombineAbilityUI _combineAbilityPrefab;
    [SerializeField] private Text _combineText;
    [SerializeField] private RectTransform _combineAbilitiesParent;

    [Inject] private AbilityGiver _abilityGiver;

    private AbilityContainer _ability;
    private List<LevelUI> _levels;
    private List<CombineAbilityUI> _combines;

    public void Initialize()
    {
        _levels = new List<LevelUI>();
        _combines = new List<CombineAbilityUI>();
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

        _abilityIcon.sprite = _ability.UpgradeIcon;

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

        _combineText.gameObject.SetActive(false);
        /*
        if (ability as PassiveAbility != null)
        {
            _combineText.gameObject.SetActive(true);

            if (_combines.Count > 0)
            {
                foreach (CombineAbilityUI combine in _combines)
                {
                    Destroy(combine.gameObject);
                }

                _combines.Clear();
            }

            foreach (CombineAbility combineAbility in (ability as PassiveAbility).CombinedAbilities)
            {
                CombineAbilityUI combine = Instantiate(_combineAbilityPrefab, _combineAbilitiesParent);

                combine.Initialize(combineAbility.CombinedWeapon.InventoryIcon);

                _combines.Add(combine);
            }
        }
        else
        {
            
        }*/
    }

    /// <summary>
    /// Invokes by button
    /// </summary>
    public void ChooseUpgrade()
    {
        _abilityGiver.GetAbility(_ability);
    }
}
