using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GOReanimationMenu : UIMenu
{
    [SerializeField][Range(5, 20)] private int _reanimationMaxTimer = 10;

    [Space(5)]
    [SerializeField] private Text _timerText;

    private int _timer; 

    public override void Display(bool playAnimation = false)
    {
        base.Display(playAnimation);

        _timer = _reanimationMaxTimer;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        _timerText.text = _timer.ToString();

        if (_timer <= 0)
        {
            (_parentMenu as GameOverMenu).OnClose();
            yield return null;
        }
        else
        {
            yield return new WaitForSecondsRealtime(1);

            _timer--;
            StartCoroutine(UpdateTimer());
        }
    }
}
