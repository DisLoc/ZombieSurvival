using System.Collections.Generic;
using UnityEngine;

public class CampMenu : UIMenu
{
    [Header("Camp menu settings")]
    [SerializeField] private CampTalentsMenu _talents;
    [SerializeField] private CampUpgradesMenu _upgrades;
    [SerializeField] private List<UpgradableBuilding> _buildings;

    public override void Display(bool playAnimation = false)
    {
        _talents.Hide();
        _upgrades.Hide();

        foreach(var building in _buildings)
        {
            building.CheckLocked();
        }

        base.Display(playAnimation);
    }

    public override void Hide(bool playAnimation = false)
    {
        _talents.Hide();
        _upgrades.Hide();

        base.Hide(playAnimation);
    }
}
