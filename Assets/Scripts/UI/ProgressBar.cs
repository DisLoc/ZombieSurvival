using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour, IGameStartHandler, IEnemyKilledHandler, IMinuteLeftHandler
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private Image _progressBar;

    [Header("Progression settings")]
    [SerializeField] private int _maxProgress = 100;
    [SerializeField][Range(1, 100)] private int _progressPerMinute = 5;
    [SerializeField][Range(1, 1000)] private int _enemiesForProgress = 75;
    [Tooltip("Additional progress each X enemies")]
    [SerializeField][Range(1, 100)] private int _progressPerEnemies = 1; 

    private int _killed;
    private int _progress;

    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    public void Start()
    {
        OnGameStart();
    }

    public void OnGameStart()
    {
        _killed = 0;
        _progress = 0;

        UpdateUI();
    }

    public void OnMinuteLeft()
    {
        if (_isDebug) Debug.Log("Minute left, add progress: " + _progressPerMinute);

        _progress += _progressPerMinute;
        UpdateUI();
    }

    public void OnEnemyKilled()
    {
        if (_isDebug) Debug.Log("Enemy killed");

        _killed++;
        if (_killed >= _enemiesForProgress)
        {
            Debug.Log("Enemies killed, add progress: " + _progressPerEnemies);

            _killed = 0;
            _progress += _progressPerEnemies;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (_isDebug) Debug.Log("Update progress bar");

        _progressBar.fillAmount = (float)_progress / _maxProgress;
        
        if (_progress >= _maxProgress)
        {
            if (_isDebug) Debug.Log("Level complete!");

            EventBus.Publish<IGameOverHandler>(handler => handler.OnGameOver());
        }
    }
}
