using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class PauseMenu : UIMenu
{
    public override void Display(bool playAnimation = false)
    {
        base.Display(playAnimation);
    }

    public override void Hide(bool playAnimation = false)
    {
        base.Hide(playAnimation);
    }

    public void OnContinue()
    {
        Hide(true);
    }

    public void OnExit()
    {
        SceneManager.LoadScene(0);
    }
}
