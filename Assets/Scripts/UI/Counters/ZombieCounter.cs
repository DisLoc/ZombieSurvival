using UnityEngine;
using UnityEngine.UI;

public sealed class ZombieCounter : MonoBehaviour, IEnemyKilledHandler, IGameStartHandler
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private Text _countText;

    private int _killed;

    public int TotalKilled => _killed;

    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    public void OnGameStart()
    {
        _killed = 0;
    }

    public void OnEnemyKilled(Enemy enemy)
    {
        _killed++;

        _countText.text = _killed.ToString();
    }
}
