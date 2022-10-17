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
    [SerializeField] private AvailableAbilities _abilities;

    [Inject] private Player _player;

    private void OnEnable()
    {
        EventBus.Subscribe(this);
        
        _menuGO.SetActive(false);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    public void OnPlayerLevelUp()
    {
        //_menuGO.SetActive(true);

        foreach (AbilityUI ability in _abilitiesUI)
        {
            ability.Initialize(GetRandomAbility());
        }
    }

    private AbilityContainer GetRandomAbility()
    {
        return _abilities.Abilities[0];
    }

    public void GetAbility(AbilityContainer ability)
    {
        _menuGO.SetActive(false);

        _player.GetAbility(ability);
    }
}
