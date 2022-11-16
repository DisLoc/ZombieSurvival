using UnityEngine;

public class ItemUpgradeMenu : UIMenu
{
    [Header("Upgrade menu settings")]
    [SerializeField] private ItemResetMenu _resetMenu;

    private Equipment _equipment;

    public override void Initialize(MainMenu mainMenu, UIMenu parentMenu = null)
    {
        base.Initialize(mainMenu, parentMenu);

        _resetMenu.Initialize(mainMenu, this);
    }

    public override void Display(bool playAnimation = false)
    {
        _resetMenu.Hide();

        base.Display(playAnimation);
    }

    public override void Hide(bool playAnimation = false)
    {
        _resetMenu.Hide();

        base.Hide(playAnimation);
    }
    public void SetEquipment(Equipment equipment)
    {
        _equipment = equipment;
    }

    public void OnResetClick()
    {
        base.Hide(true);

        _resetMenu.SetEquipment(_equipment);
        _resetMenu.Display(true);
    }

    public void OnCloseMenu()
    {
        Hide(true);
    }
}
