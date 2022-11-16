using UnityEngine;

public sealed class InventoryMenu : UIMenu
{
    [Header("Inventory menu settings")]
    [SerializeField] private ItemUpgradeMenu _upgradeMenu;

    public override void Initialize(MainMenu mainMenu, UIMenu parentMenu = null)
    {
        base.Initialize(mainMenu, parentMenu);

        _upgradeMenu.Initialize(mainMenu, this);
    }

    public override void Display(bool playAnimation = false)
    {
        _upgradeMenu.Hide();

        base.Display(playAnimation);
    }

    public override void Hide(bool playAnimation = false)
    {
        _upgradeMenu.Hide();

        base.Hide(playAnimation);
    }

    public void OnItemClick(EquipmentSlot slot)
    {
        _upgradeMenu.SetEquipment(slot.Equipment);
        _upgradeMenu.Display(true);
    }
}
