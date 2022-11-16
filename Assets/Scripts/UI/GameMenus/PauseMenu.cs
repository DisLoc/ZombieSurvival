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
        _mainMenu.DisplayDefault();
    }

    public void OnExit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
