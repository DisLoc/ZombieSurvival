using UnityEngine;

public class ItemResetMenu : UIMenu
{
    private Equipment _equipment;

    public void SetEquipment(Equipment equipment)
    {
        _equipment = equipment;
    }

    public void OnCloseMenu()
    {
        (_parentMenu as ItemUpgradeMenu).Display();

        Hide(true);
    }
}
