using UnityEngine;
using UnityEngine.UI;

public class ExpBar : FillBar
{
    [Header("Settings")]
    [SerializeField] private Text _levelText;
    private PlayerExpLevel _expirience;

    public void Initialize(PlayerExpLevel expLevel)
    {
        _expirience = expLevel;

        UpdateLevel();
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
        _fillBar.maxValue = _maxFillValue;
        _levelText.text = ((int)_expirience.Value).ToString();
    }
}
