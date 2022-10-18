using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AbilityGiver : MonoBehaviour, IPlayerLevelUp
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private GameObject _menuGO;
    [SerializeField] private List<AbilityUI> _abilitiesUI;

    [Space(5)]
    [SerializeField] private AvailableAbilities _availableAbilities;

    private List<AbilityContainer> _abilities;

    [Inject] private Player _player;

    private void OnEnable()
    {
        EventBus.Subscribe(this);
        
        _menuGO.SetActive(false);

        _abilities = new List<AbilityContainer>(_availableAbilities.Abilities);

        foreach(AbilityUI abilityUI in _abilitiesUI)
        {
            abilityUI.Initialize();
        }
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    public void OnPlayerLevelUp()
    {
        _menuGO.SetActive(true);

        int i = 0; // test

        foreach (AbilityUI ability in _abilitiesUI)
        {
            ability.SetAbility(GetRandomAbility(i++));
        }
    }

    private AbilityContainer GetRandomAbility()
    {
        return _abilities[0];
    }
    
    private AbilityContainer GetRandomAbility(int index) // test
    {
        return _abilities[index];
    }

    public void GetAbility(AbilityContainer ability)
    {
        _menuGO.SetActive(false);

        _player.GetAbility(ability);
    }

    public void GetAbilityUpgrade(Upgrade upgrade)
    {
        _menuGO.SetActive(false);

        _player.GetUpgrade(upgrade);
    }
}
