using UnityEngine;

public sealed class PauseMenu : UIMenu
{
    public override void Display(bool playAnimation = false)
    {
        Time.timeScale = 0;

        base.Display(playAnimation);
    }

    public override void Hide(bool playAnimation = false)
    {
        Time.timeScale = 1;

        base.Hide(playAnimation);
    }
}
