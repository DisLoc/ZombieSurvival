using UnityEngine;

public sealed class LevelProgress : FillBar, IGameStartHandler, IEnemyKilledHandler, IMinuteLeftHandler
{
    [SerializeField][Range(1, 100)] private int _progressPerMinute = 5;
    [SerializeField][Range(1, 1000)] private int _enemiesForProgress = 75;
    [Tooltip("Additional progress each X enemies")]
    [SerializeField][Range(1, 100)] private int _progressPerEnemies = 1; 

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

        base.Initialize();
    }

    public void Start()
    {
        Initialize();
        OnGameStart();
    }

    public void OnGameStart()
    {
        _killed = 0;
        _value = 0;

        UpdateBar();
    }

    public void OnMinuteLeft()
    {
        if (_isDebug) Debug.Log("Minute left, add progress: " + _progressPerMinute);

        _value += _progressPerMinute;
        UpdateBar();
    }

    public void OnEnemyKilled()
    {
        if (_isDebug) Debug.Log("Enemy killed");

        _killed++;
        if (_killed >= _enemiesForProgress)
        {
            Debug.Log("Enemies killed, add progress: " + _progressPerEnemies);

            _killed = 0;
            _value += _progressPerEnemies;
        }

        UpdateBar();
    }

    protected override void UpdateBar()
    {
        if (_isDebug) Debug.Log("Update progress bar");

        base.UpdateBar();
        
        if (_value >= _maxFillValue)
        {
            if (_isDebug) Debug.Log("Level complete!");

            EventBus.Publish<IGameOverHandler>(handler => handler.OnGameOver());
        }
    }
}
