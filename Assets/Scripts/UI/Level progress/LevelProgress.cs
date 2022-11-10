using UnityEngine;
using Zenject;

public sealed class LevelProgress : FillBar, IGameStartHandler
{
    [Header("LevelProgress settings")]
    [SerializeField] private SurvivalTimeCounter _survivalTimeCounter;

    private int _maxLevelTime;

    [Inject] private LevelContext _levelContext;

    public int Value => _value;

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
        
        _maxLevelTime = _levelContext.LevelLenght;
    }

    public void OnTimerUpdate()
    {
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
        
        if (_value >= _maxFillValue)
        {
            if (_isDebug) Debug.Log("Level complete!");

            EventBus.Publish<ILevelPassedHandler>(handler => handler.OnLevelPassed());
        }
    }
}
