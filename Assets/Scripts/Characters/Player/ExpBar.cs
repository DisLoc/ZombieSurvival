using UnityEngine;
using UnityEngine.UI;

public class ExpBar : FillBar
{
    [Header("Settings")]
    [SerializeField] private Text _levelText;
    private ExpLevel _expirience;
    

    public void Initialize(ExpLevel expLevel)
    {
        _expirience = expLevel;

        UpdateExp();
    }

    public void UpdateExp()
    {
        _value = (int)_expirience.Exp.Value;

        UpdateBar();
    }

    public void UpdateLevel()
    {
        _maxFillValue = _expirience.ExpForLevel;
        _levelText.text = ((int)_expirience.Value).ToString();
    }
}
