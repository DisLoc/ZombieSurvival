using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public sealed class PauseMenu : UIMenu
{
    [SerializeField] private AbilitySlot _abilitySlotPrefab;
    [SerializeField] private Transform _weaponSlotsParent;
    [SerializeField] private Transform _passiveSlotsParent;
 
    [Inject] private Player _player;

    private List<AbilitySlot> _weaponSlots;
    private List<AbilitySlot> _passiveSlots;

    public override void Initialize(MainMenu mainMenu, UIMenu parentMenu = null)
    {
        base.Initialize(mainMenu, parentMenu);

        _weaponSlots = new List<AbilitySlot>(_player.AbilityInventory.MaxActiveAbilitiesCount);
        _passiveSlots = new List<AbilitySlot>(_player.AbilityInventory.MaxPassiveAbilitiesCount);

        for(int i = 0; i < _player.AbilityInventory.MaxActiveAbilitiesCount; i++)
        {
            _weaponSlots.Add(Instantiate(_abilitySlotPrefab, _weaponSlotsParent));
            _weaponSlots[i].gameObject.SetActive(false);
        }
        
        for(int i = 0; i < _player.AbilityInventory.MaxPassiveAbilitiesCount; i++)
        {
            _passiveSlots.Add(Instantiate(_abilitySlotPrefab, _passiveSlotsParent));
            _passiveSlots[i].gameObject.SetActive(false);
        }
    }

    public override void Display(bool playAnimation = false)
    {
        UpdateInventory();

        base.Display(playAnimation);
    }

    public override void Hide(bool playAnimation = false)
    {
        base.Hide(playAnimation);

        for (int i = 0; i < _weaponSlots.Count; i++)
        {
            _weaponSlots[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < _passiveSlots.Count; i++)
        {
            _passiveSlots[i].gameObject.SetActive(false);
        }
    }

    public void OnContinue()
    {
        _mainMenu.DisplayDefault();
    }

    public void OnExit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void UpdateInventory()
    {
        for (int i = 0; i < _player.AbilityInventory.Weapons.Count; i++)
        {
            _weaponSlots[i].gameObject.SetActive(true);
            _weaponSlots[i].Initialize(_player.AbilityInventory.Weapons[i]);
        }

        for (int i = 0; i < _player.AbilityInventory.PassiveAbilities.Count; i++)
        {
            _passiveSlots[i].gameObject.SetActive(true);
            _passiveSlots[i].Initialize(_player.AbilityInventory.PassiveAbilities[i]);
        }
    }
}
