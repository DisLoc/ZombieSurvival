using UnityEngine;

public class HPBar : FillBar, IGameOverHandler
{
    private HealthPoint _health;

    public void Initialize(HealthPoint health)
    {
        _maxFillValue = (int)health.MaxValue;
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

        UpdateBar();
    }

    public void OnGameOver()
    {
        if (_isDebug) Debug.Log(name + " game over");
    }
}
