using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AbilityGiver : MonoBehaviour, IPlayerLevelUpHandler
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private UIMenu _menu;

    [Space(5)]
    [SerializeField] private RectTransform _abilityUIParent;
    [SerializeField] private AbilityUI _abilityUIPrefab;

    [Space(5)]
    [SerializeField] private AvailableAbilities _availableAbilities;

    private AbilitiesPerChoice _abilitiesPerChoice;
    private AbilityChooseCount _abilityChooseCount;
    private AbilitiesRerollCount _abilitiesRerollCount;

    private List<AbilityUI> _abilitiesUI;
    private List<AbilityContainer> _abilities;
    
    private int _levelUps;
    private bool _onChoice;

    [Inject] private Player _player;

    private void OnEnable()
    {
        EventBus.Subscribe(this);

        _abilityChooseCount = (_player.Stats as PlayerStats).AbilityChooseCount;
        _abilitiesPerChoice = (_player.Stats as PlayerStats).AbilitiesPerChoice;
        _abilitiesRerollCount = (_player.Stats as PlayerStats).AbilitiiesRerollCount;

        _abilities = new List<AbilityContainer>(_availableAbilities.Abilities);

        InitializeAbilitiesUI();

        _levelUps = 0;
        _onChoice = false;
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    [ContextMenu("Get ability")]
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
        _menu.MainMenuDisplay();

        InitializeAbilitiesUI();

        List<AbilityContainer> abilities = GetRandomAbilities((int)_abilitiesPerChoice.Value);

        for(int i = 0; i < (int)_abilitiesPerChoice.Value; i++)
        {
            if (i >= abilities.Count || i > _abilitiesUI.Count)
            {
                if (_isDebug) Debug.Log("Abilities error!");

                Time.timeScale = 1;
                _onChoice = false;

                _menu.MainMenuHide();
                return;
            }

            _abilitiesUI[i].SetAbility(abilities[i]);
        }
    }

    /// <summary>
    /// Create AbilitiesUI based on AbilitiesPerLevel
    /// </summary>
    private void InitializeAbilitiesUI()
    {
        if (_abilitiesUI != null && _abilitiesUI.Count != (int)_abilitiesPerChoice.Value)
        {
            foreach(AbilityUI abilityUI in _abilitiesUI)
            {
                Destroy(abilityUI.gameObject);
            }

            _abilitiesUI.Clear();

            for (int i = 0; i < (int)_abilitiesPerChoice.Value; i++)
            {
                AbilityUI abilityUI = Instantiate(_abilityUIPrefab, _abilityUIParent);

                abilityUI.Initialize(this);

                _abilitiesUI.Add(abilityUI);
            }
        }
        else if (_abilitiesUI == null)
        {
            _abilitiesUI = new List<AbilityUI>((int)_abilitiesPerChoice.Value);

            for (int i = 0; i < (int)_abilitiesPerChoice.Value; i++)
            {
                AbilityUI abilityUI = Instantiate(_abilityUIPrefab, _abilityUIParent);

                abilityUI.Initialize(this);

                _abilitiesUI.Add(abilityUI);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Return X random abilities based on abilities that player getted</returns>
    public List<AbilityContainer> GetRandomAbilities(int count)
    {
        CleanupAbilities();

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
                        if (_isDebug) Debug.Log("Add super to pool: " + super.Name);
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
                randomAbility = _availableAbilities.AdditionalAbilities[UnityEngine.Random.Range(0, _availableAbilities.AdditionalAbilities.Count)];
            }
            else
            {
                do 
                {
                    randomAbility = _abilities[UnityEngine.Random.Range(0, _abilities.Count)];
                } while (randomAbilities.Contains(randomAbility)); // get random ability without repeating
            }

            randomAbilities.Add(randomAbility);
        }

        return randomAbilities;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Return X abilities upgrades based on abilities that player getted</returns>
    public List<AbilityContainer> GetAbilitiesUpgrades(int count)
    {
        List<AbilityContainer> abilitiesUpgrades = new List<AbilityContainer>();
        AbilityInventory playerInventory = _player.AbilityInventory;

        foreach (AbilityContainer abilityContainer in playerInventory.Abilities) // add abilities that player have in inventory
        {
            if (!abilityContainer.IsMaxLevel) abilitiesUpgrades.Add(abilityContainer);

            else
            {
                if (abilityContainer.IsMaxLevel && abilityContainer as Weapon != null) // weapons at max level can upgrade to super
                {
                    Weapon super = playerInventory.FindCombine(abilityContainer as Weapon);

                    if (super != null)
                    {
                        if (_isDebug) Debug.Log("Add super upgrade to pool: " + super.Name);
                        abilitiesUpgrades.Add(super);
                    }
                }
            }
        }

        List<AbilityContainer> randomUpgrades = new List<AbilityContainer>(count);

        for (int currentCount = 0; currentCount < count; currentCount++)
        {
            AbilityContainer randomAbility;

            if (abilitiesUpgrades.Count <= randomUpgrades.Count) // add additional abilities (like more gold or heal)
            {
                randomAbility = _availableAbilities.AdditionalAbilities[UnityEngine.Random.Range(0, _availableAbilities.AdditionalAbilities.Count)];
            }
            else
            {
                do
                {
                    randomAbility = abilitiesUpgrades[UnityEngine.Random.Range(0, abilitiesUpgrades.Count)];
                } while (randomUpgrades.Contains(randomAbility)); // get random ability without repeating
            }

            randomUpgrades.Add(randomAbility);
        }

        return randomUpgrades;
    }

    public Weapon GetRandomWeapon(List<Type> concreteWeaponTypes = null)
    {
        List<AbilityContainer> weapons = _abilities.FindAll(item => item as Weapon != null && (item as Weapon).IsSuper == false);

        if (concreteWeaponTypes != null)
        {
            weapons.RemoveAll(item => !concreteWeaponTypes.Contains(item.GetType()));
        }

        return weapons[UnityEngine.Random.Range(0, weapons.Count)] as Weapon;
    }

    /// <summary>
    /// Add ability to player
    /// </summary>
    /// <param name="ability">Ability need to add</param>
    public void GetAbility(AbilityContainer ability)
    {
        _player.GetAbility(ability);

        _levelUps--;

        if (_levelUps > 0)
        {
            SetAbilities();
        }
        else
        {
            _menu.MainMenuHide(); ;

            _levelUps = 0;
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
