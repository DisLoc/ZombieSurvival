using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void OnContinue()
    {
        Hide(true);
    }

    public void OnExit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
