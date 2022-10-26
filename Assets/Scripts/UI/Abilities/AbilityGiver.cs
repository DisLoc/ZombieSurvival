using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AbilityGiver : MonoBehaviour, IPlayerLevelUp
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private GameObject _menuGO;

    [Space(5)]
    [SerializeField] private RectTransform _abilityUIParent;
    [SerializeField] private AbilityUI _abilityUIPrefab;

    [Header("Stats settings")]
    [SerializeField] private AbilitiesPerLevel _abilitiesPerLevel;
    [SerializeField] private AbilityChooseCount _abilityChooseCount;

    [Space(5)]
    [SerializeField] private AvailableAbilities _availableAbilities;


    private List<AbilityUI> _abilitiesUI;
    private List<AbilityContainer> _abilities;
    private int _levelUps;
    private bool _onChoice;

    [Inject] private Player _player;

    private void OnEnable()
    {
        EventBus.Subscribe(this);

        _abilitiesPerLevel.Initialize();
        _abilityChooseCount.Initialize();

        _abilities = new List<AbilityContainer>(_availableAbilities.Abilities);
        _abilitiesUI = new List<AbilityUI>((int)_abilitiesPerLevel.Value);

        InitializeAbilitiesUI();

        _menuGO.SetActive(false);

        _levelUps = 0;
        _onChoice = false;
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    public void OnPlayerLevelUp()
    {
        _levelUps += (int)_abilityChooseCount.Value;
        
        if (!_onChoice)
        {
            SetAbilities();
        }

        _onChoice = true;
    }

    /// <summary>
    /// Enable ability menu for choosing new ability or upgrade existing
    /// </summary>
    public void SetAbilities()
    {
        _menuGO.SetActive(true);
        CleanupAbilities();

        InitializeAbilitiesUI();

        List<AbilityContainer> abilities = GetRandomAbilities((int)_abilitiesPerLevel.Value);

        if (_abilitiesUI.Count != abilities.Count)
        {
            if (_isDebug) Debug.Log("Abilities count error!");

            _menuGO.SetActive(false);
            return;
        }

        for(int i = 0; i < (int)_abilitiesPerLevel.Value; i++)
        {
            if (_abilitiesUI[i].gameObject.activeSelf)
            {
                _abilitiesUI[i].SetAbility(abilities[i]);
            }
            else if (_isDebug) Debug.Log("AbilityUI is disabled! Skip it");
        }
    }

    /// <summary>
    /// Create AbilitiesUI based on AbilitiesPerLevel
    /// </summary>
    private void InitializeAbilitiesUI()
    {
        if (_abilitiesUI.Count > 0)
        {
            foreach(AbilityUI abilityUI in _abilitiesUI)
            {
                Destroy(abilityUI.gameObject);
            }

            _abilitiesUI.Clear();
        }

        for (int i = 0; i < (int)_abilitiesPerLevel.Value; i++)
        {
            AbilityUI abilityUI = Instantiate(_abilityUIPrefab, _abilityUIParent);

            abilityUI.Initialize(this);

            _abilitiesUI.Add(abilityUI);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Return X random abilities based on abilities that player getted</returns>

    private List<AbilityContainer> GetRandomAbilities(int count)
    {
        AbilityInventory playerInventory = _player.AbilityInventory;

        foreach(AbilityContainer abilityContainer in playerInventory.Abilities) // add abilities that player have in inventory
        {
            if (!abilityContainer.IsMaxLevel) _abilities.Add(abilityContainer);

            else
            {
                if (abilityContainer.IsMaxLevel && abilityContainer as Weapon != null) // weapons at max level can upgrade to super
                {
                    Weapon super = playerInventory.FindCombine(abilityContainer as Weapon);

                    if (super != null)
                    {
                        _abilities.Add(super);
                    }
                }
            }
        }

        List<AbilityContainer> randomAbilities = new List<AbilityContainer>(count);

        for (int currentCount = 0; currentCount < count; currentCount++)
        {
            AbilityContainer randomAbility;

            if (_abilities.Count <= randomAbilities.Count) // add additional abilities (like more gold or heal)
            {
                randomAbility = _availableAbilities.AdditionalAbilities[Random.Range(0, _availableAbilities.AdditionalAbilities.Count)];
            }
            else
            {
                do 
                {
                    randomAbility = _abilities[Random.Range(0, _abilities.Count)];
                } while (randomAbilities.Contains(randomAbility)); // get random ability without repeating
            }

            randomAbilities.Add(randomAbility);
        }

        return randomAbilities;
    }

    /// <summary>
    /// Add ability to player
    /// </summary>
    /// <param name="ability">Ability need to add</param>
    public void GetAbility(AbilityContainer ability)
    {
        _menuGO.SetActive(false);

        _player.GetAbility(ability);

        CleanupAbilities();

        _levelUps--;

        if (_levelUps > 0)
        {
            SetAbilities();
        }
        else
        {
            _onChoice = false;
        }
    }

    /// <summary>
    /// Removes all abilities that player have in inventory. Also removes Passive/Active abilities if reached max count
    /// </summary>
    private void CleanupAbilities()
    {
        _abilities.RemoveAll(item => _player.AbilityInventory.Find(item) != null);

        if (_player.AbilityInventory.PassiveAbilitiesCount >= _player.AbilityInventory.MaxPassiveAbilitiesCount)
        {
            _abilities.RemoveAll(item => item as PassiveAbility != null);
        }

        if (_player.AbilityInventory.ActiveAbilitiesCount >= _player.AbilityInventory.MaxActiveAbilitiesCount)
        {
            _abilities.RemoveAll(item => item as Weapon != null);
        }
    }
}
