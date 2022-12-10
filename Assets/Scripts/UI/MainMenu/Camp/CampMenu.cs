using UnityEngine;

public class CampMenu : UIMenu
{
    [SerializeField] private CampTalentsMenu _talents;
    [SerializeField] private CampUpgradesMenu _upgrades;

    public override void Display(bool playAnimation = false)
    {
        _talents.Hide();
        _upgrades.Hide();

        base.Display(playAnimation);
    }

    public override void Hide(bool playAnimation = false)
    {
        _talents.Hide();
        _upgrades.Hide();

        base.Hide(playAnimation);
    }
}
