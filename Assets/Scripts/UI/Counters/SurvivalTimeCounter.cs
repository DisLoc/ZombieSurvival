using UnityEngine;
using UnityEngine.UI;

public class SurvivalTimeCounter : MonoBehaviour, IGameStartHandler
{
    [SerializeField] private Text _survivalTimeText;

    private float _survivalTime;
    private float _seconds;

    public void OnGameStart()
    {
        _survivalTime = 0f;
        _seconds = 0f;
    }

    private void FixedUpdate()
    {
        _seconds += Time.fixedDeltaTime;
        _survivalTime += Time.fixedDeltaTime;

        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        _survivalTimeText.text = ((int)_survivalTime).ToString();

        if (_seconds >= 60)
        {
            _seconds = 0f;
            EventBus.Publish<IMinuteLeftHandler>(handler => handler.OnMinuteLeft());
        }
    }
}
