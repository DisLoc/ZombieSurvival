using UnityEngine;

public class ExpBar : FillBar
{
    private ExpLevel _expirience;

    public void Initialize(ExpLevel expLevel)
    {
        _expirience = expLevel;

        UpdateExp();
    }

    public void UpdateExp()
    {
        _value = (int)_expirience.Exp.Value;
        _maxFillValue = _expirience.ExpForLevel;

        UpdateBar();
    }
}
