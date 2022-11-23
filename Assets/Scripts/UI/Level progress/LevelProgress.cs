using UnityEngine;
using Zenject;

public sealed class LevelProgress : FillBar, IGameStartHandler, IBossEventHandler, IBossEventEndedHandler
{
    [Header("LevelProgress settings")]
    [SerializeField] private SurvivalTimeCounter _survivalTimeCounter;
    [SerializeField] private LevelProgressBreakpoint _breakpointPrefab;

    private int _maxLevelTime;
    private bool _onBossEvent;

    public int Value => _value;

    [Inject] private LevelContext _levelContext;

    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    public override void Initialize()
    {
        _value = _minFillValue;

        base.Initialize();
    }

    public void OnGameStart()
    {
        Initialize();

        foreach (Breakpoint breakpoint in _levelContext.AllBreakpoints.Breakpoints)
        {
            if (breakpoint.Icon != null)
            {
                LevelProgressBreakpoint newBreakpoint = Instantiate(_breakpointPrefab, transform);

                newBreakpoint.Transform.anchorMin = new Vector2(breakpoint.RequiredProgress * 0.01f, 0f);
                newBreakpoint.Transform.anchorMax = new Vector2(breakpoint.RequiredProgress * 0.01f, 1f);

                newBreakpoint.Transform.anchoredPosition = new Vector2(0f, 0f);

                newBreakpoint.SetBreakpoint(breakpoint);
            }
        }
        
        _maxLevelTime = _levelContext.LevelLenght;
    }

    public void OnBossEvent()
    {
        _onBossEvent = true;
    }

    public void OnBossEventEnd()
    {
        _onBossEvent = false;

        if (_value >= _maxFillValue)
        {
            if (_isDebug) Debug.Log("Level complete!");

            EventBus.Publish<ILevelPassedHandler>(handler => handler.OnLevelPassed());
        }
    }

    public void OnTimerUpdate()
    {
        if (_onBossEvent) return;

        int newVal = (int)(_survivalTimeCounter.SurvivalTime / _maxLevelTime * _maxFillValue);
        
        if (_value != newVal)
        {
            _value = newVal;

            UpdateBar();
        }
    }

    protected override void UpdateBar()
    {
        if (_isDebug) Debug.Log("Update progress bar");

        base.UpdateBar();

        EventBus.Publish<ILevelProgressUpdateHandler>(handler => handler.OnLevelProgressUpdate(_value));
        
        if (_value >= _maxFillValue && !_onBossEvent)
        {
            if (_isDebug) Debug.Log("Level complete!");

            EventBus.Publish<ILevelPassedHandler>(handler => handler.OnLevelPassed());
        }
    }
}
