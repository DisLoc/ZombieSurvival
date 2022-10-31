using UnityEngine;

public class HPBar : FillBar
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

    public void UpdateHealth()
    {
        _value = (int)_health.Value;
        _maxFillValue = _health.MaxHP;

        base.Initialize();
    }

    public void OnGameOver()
    {
        if (_isDebug) Debug.Log(name + " game over");
    }
}
