using UnityEngine;
using UnityEngine.UI;

public class SurvivalTimeCounter : MonoBehaviour, IGameStartHandler, IBossEventHandler, IBossEventEndedHandler
{
    [SerializeField] private LevelProgress _levelProgress;
    [SerializeField] private Text _survivalTimeText;

    private float _survivalTime; 
    private bool _onBossEvent;

    public float SurvivalTime => _survivalTime;

    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    private void FixedUpdate()
    {
        if (_onBossEvent) return;

        _survivalTime += Time.fixedDeltaTime;

        _levelProgress.OnTimerUpdate();

        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        _survivalTimeText.text = IntegerFormatter.GetTime((int)_survivalTime);
    }

    public void OnGameStart()
    {
        _survivalTime = 0f;
        UpdateTimerText();
    }

    public void OnBossEvent()
    {
        _onBossEvent = true;
    }

    public void OnBossEventEnd()
    {
        _onBossEvent = false;
    }
}
