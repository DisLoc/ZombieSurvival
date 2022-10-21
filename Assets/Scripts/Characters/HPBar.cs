using UnityEngine;

public class HPBar : FillBar, IGameOverHandler
{
    private Health _health;

    public void Initialize(Health health)
    {
        _maxFillValue = health.MaxHP;
        _minFillValue = (int)health.MinValue;
        _value = _maxFillValue;

        _health = health;

        base.Initialize();
    }

    protected void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    public void UpdateHealth()
    {
        _value = (int)_health.Value;
        _maxFillValue = _health.MaxHP;

        UpdateBar();
    }

    public void OnGameOver()
    {
        if (_isDebug) Debug.Log(name + " game over");
    }
}
