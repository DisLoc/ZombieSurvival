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

    [Inject] private AbilityGiver _abilityGiver;

    private AbilityContainer _ability;
    private List<LevelUI> _levels;

    public void Initialize()
    {
        _levels = new List<LevelUI>();
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

        _abilityIcon.sprite = _ability.UpgradeData.UpgradeIcon;

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
    }

    /// <summary>
    /// Invokes by button
    /// </summary>
    public void ChooseUpgrade()
    {
        _abilityGiver.GetAbility(_ability);
    }
}
