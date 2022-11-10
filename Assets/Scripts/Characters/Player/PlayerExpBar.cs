using UnityEngine;
using UnityEngine.UI;

public class PlayerExpBar : FillBar
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

    /// <summary>
    /// Update Slider Value
    /// </summary>
    public void UpdateExp()
    {
        _value = (int)_expirience.Exp.Value;

        UpdateBar();
    }

    /// <summary>
    /// Update Slider MaxFillValue
    /// </summary>
    public void UpdateLevel()
    {
        _maxFillValue = _expirience.ExpForLevel;
        _fillBar.maxValue = _maxFillValue;
        _levelText.text = ((int)_expirience.Value).ToString();
    }
}
