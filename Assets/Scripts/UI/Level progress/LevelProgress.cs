using UnityEngine;
using Zenject;

public sealed class LevelProgress : FillBar, IGameStartHandler, IEnemyKilledHandler, IMinuteLeftHandler
{
    [SerializeField][Range(1, 100)] private int _progressPerMinute = 5;
    [SerializeField][Range(1, 1000)] private int _enemiesForProgress = 75;
    [Tooltip("Additional progress each X enemies")]
    [SerializeField][Range(1, 100)] private int _progressPerEnemies = 1;

    [Inject] private LevelContext _levelContext;

    public float Value => _value;

    private int _killed;

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
        _killed = 0;

        base.Initialize();
    }

    public void OnGameStart()
    {
        Initialize();
    }

    public void OnMinuteLeft()
    {
        if (_isDebug) Debug.Log("Minute left, add progress: " + _progressPerMinute);

        _value += _progressPerMinute;
        UpdateBar();
    }

    public void OnEnemyKilled(Zombie enemy)
    {
        if (_isDebug) Debug.Log("Enemy killed");

        _killed++;
        if (_killed >= _enemiesForProgress)
        {
            if (_isDebug) Debug.Log("Enemies killed, add progress: " + _progressPerEnemies);

            _killed = 0;
            _value += _progressPerEnemies;
        }

        UpdateBar();
    }

    protected override void UpdateBar()
    {
        if (_isDebug) Debug.Log("Update progress bar");

        base.UpdateBar();

        EventBus.Publish<ILevelProgressUpdateHandler>(handler => handler.OnLevelProgressUpdate(_value));
        
        if (_value >= _maxFillValue)
        {
            if (_isDebug) Debug.Log("Level complete!");

            EventBus.Publish<IGameOverHandler>(handler => handler.OnGameOver());
        }
    }
}
