using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class InventoryMenu : UIMenu
{
    [Header("Player animation")]
    [SerializeField] private Animator _playerAnimator;
    private enum AnimationStates
    {
        WithBlade,
        WithShotgun,
        WithPistol,

        Girl
    }

    private void SetAnimatorBools()
    {
        _playerAnimator.SetBool(AnimationStates.Girl.ToString(), true); // TODO add more characters

        if (_equipmentInventory == null) return;

        WeaponEquipment weapon = _equipmentInventory[EquipSlot.Weapon] as WeaponEquipment;
        if (weapon != null)
        {
            if (weapon.BaseWeapon as Shotgun != null)
            {
                _playerAnimator.SetBool(AnimationStates.WithShotgun.ToString(), true);
            }
            else if (weapon.BaseWeapon as Blade != null)
            {
                _playerAnimator.SetBool(AnimationStates.WithBlade.ToString(), true);
            }
            else if (weapon.BaseWeapon as Gun != null)
            {
                _playerAnimator.SetBool(AnimationStates.WithPistol.ToString(), true);
            }
        }
        else
        {
            _playerAnimator.SetBool(AnimationStates.WithShotgun.ToString(), false);
            _playerAnimator.SetBool(AnimationStates.WithBlade.ToString(), false);
            _playerAnimator.SetBool(AnimationStates.WithPistol.ToString(), false);
        }
    }

    [Header("Inventory menu settings")]
    [SerializeField] private ItemUpgradeMenu _upgradeMenu;
    [SerializeField] private LevelContextInstaller _contextInstaller;
    [SerializeField] private RectTransform _unequippedInventoryTransform;
    [SerializeField] private GridLayoutGroup _grid;

    [Space(5)]
    [SerializeField] private MergeMenu _mergeMenu;
    [SerializeField] private HeroListMenu _heroListMenu;

    [Header("Equipment settings")]
    [SerializeField] private EquipmentSlot _equipmentSlotPrefab;
    [SerializeField] private Transform _unequippedEquipmentParent;

    [SerializeField] private EquipmentTypesData _equipmentTypesData;

    [SerializeField] private List<EquipmentSlot> _slots;
    [SerializeField] private Text _damageText;
    [SerializeField] private Text _healthText;

    private UnequippedEquipmentInventory _unequippedInventory;
    private EquippedEquipmentInventory _equipmentInventory;

    public EquippedEquipmentInventory EquipmentInventory => _equipmentInventory;
    public UnequippedEquipmentInventory UnequippedInventory => _unequippedInventory;

    public EquipmentTypesData EquipmentTypesData => _equipmentTypesData;

    [Inject] private MainInventory _mainInventory;

    [Header("Test")]
    [SerializeField] private Player _player;

    public override void Initialize(MainMenu mainMenu, UIMenu parentMenu = null)
    {
        base.Initialize(mainMenu, parentMenu);

        _upgradeMenu.Initialize(mainMenu, this);
        _mergeMenu.Initialize(mainMenu, this);
        _heroListMenu.Initialize(mainMenu, this);

        _equipmentInventory = new EquippedEquipmentInventory();
        _unequippedInventory = new UnequippedEquipmentInventory(_equipmentSlotPrefab, _unequippedEquipmentParent, this);

        foreach (EquipmentSlot slot in _slots)
        {
            slot.Initialize(_equipmentTypesData);
        }

        SetAnimatorBools();
    }

    private void Start()
    {
        LoadEquipment();
    }

    public override void Display(bool playAnimation = false)
    {
        LoadEquipment();

        _upgradeMenu.Hide();
        _heroListMenu.Hide();
        _mergeMenu.Hide();

        if (_canvasGroup.alpha == 0)
        {
            base.Display(playAnimation);
        }

        UpdateValues();
    }

    public override void Hide(bool playAnimation = false)
    {
        _upgradeMenu.Hide();
        _heroListMenu.Hide();
        _mergeMenu.Hide();

        base.Hide(playAnimation);
    }

    public void OnItemClick(EquipmentSlot slot)
    {
        if (slot.Equipment == null)
        {
            if (_isDebug) Debug.Log("Missing equipment");

            return;
        }

        _upgradeMenu.SetEquipment(slot.Equipment);
        _upgradeMenu.Display(true);
    }

    public void UpdateValues()
    {
        int totalDamage = (int)_player.LevelUpgrades.GetUpgrade(1).DamageData.UpgradeValue;
        int totalHP = (int)_player.Stats.Health.MaxHealthPoints.BaseValue + (int)_player.LevelUpgrades.GetUpgrade(1).HealthData.UpgradeValue;

        foreach (EquipmentSlot slot in _slots)
        {
            if (slot.Equipment != null)
            {
                slot.SetSlot(slot.Equipment);

                if (slot.Equipment.UpgradingStat.Equals(UpgradingStat.Damage))
                {
                    foreach (UpgradeData data in slot.Equipment.EquipUpgrade.Upgrades)
                    {
                        totalDamage += (int)data.UpgradeValue;
                    }
                }

                else if (slot.Equipment.UpgradingStat.Equals(UpgradingStat.Health))
                {
                    foreach (UpgradeData data in slot.Equipment.EquipUpgrade.Upgrades)
                    {
                        totalHP += (int)data.UpgradeValue;
                    }
                }

                else if (_isDebug) Debug.Log("Equipment upgrades error!");
            }
        }

        _damageText.text = totalDamage.ToString();
        _healthText.text = totalHP.ToString();

        _unequippedInventory.UpdateInventory();
        _unequippedInventoryTransform.sizeDelta = new Vector2(0, GetInventoryHeight());
    }

    public void Equip(Equipment equipment, bool displayDefault = true)
    {
        if (equipment == null) return;

        EquipmentSlot slot = _slots.Find(item => item.ValidSlot.Equals(equipment.EquipSlot));

        if (slot.Equipment != null)
        {
            Unequip(slot.Equipment);
        }

        equipment.isEquiped = true;

        _equipmentInventory.Add(equipment);
        _unequippedInventory.RemoveEquipment(equipment);
        slot.SetSlot(equipment);

        UpdateValues();
        SetAnimatorBools();
    }

    public void Unequip(Equipment equipment)
    {
        if (equipment == null) return;

        EquipmentSlot slot = _slots.Find(item => item.Equipment != null && item.Equipment.Equals(equipment));

        if (slot != null)
        {
            _unequippedInventory.AddEquipment(equipment);
            _equipmentInventory.Remove(equipment);

            equipment.isEquiped = false;

            slot.SetSlot(null);

            UpdateValues();
            SetAnimatorBools();
        }
        else return;
    }

    public void OnMergeClick()
    {
        _upgradeMenu.Hide();
        _heroListMenu.Hide();

        _mergeMenu.Display(true);
    }

    public void OnHeroListClick()
    {
        _upgradeMenu.Hide();
        _mergeMenu.Hide();

        _heroListMenu.Display(true);
    }

    private void LoadEquipment()
    {
        if (_mainInventory == null || (_mainInventory != null && _mainInventory.EquipmentInventory.Equipment == null)) return; 
        
        foreach (Equipment equipment in _mainInventory.EquipmentInventory.Equipment)
        {
            if (equipment.isEquiped && !_equipmentInventory.Contains(equipment))
            {
                Equip(equipment);
            }
            else if (!_unequippedInventory.Equipment.Contains(equipment) && !_equipmentInventory.Contains(equipment))
            {
                _unequippedInventory.AddEquipment(equipment);
            }
        }

        UpdateValues();
    }

    private int GetInventoryHeight()
    {
        int rows = _unequippedInventory.Equipment.Count / 5 + (_unequippedInventory.Equipment.Count % 5 > 0 ? 1 : 0);

        return _grid.padding.top + _grid.padding.bottom + (int)_grid.cellSize.y * rows + (int)_grid.spacing.y * rows;
    }
}
