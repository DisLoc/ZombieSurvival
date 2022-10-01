using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class FillBar : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    [Header("Settings")]
    [SerializeField] protected Slider _fillBar;

    [Header("Filling settings")]
    [SerializeField] protected int _minFillValue = 0;
    [SerializeField] protected int _maxFillValue = 100;

    protected int _value;

    protected virtual void OnEnable()
    {
        _fillBar.minValue = _minFillValue;
        _fillBar.maxValue = _maxFillValue;

        _fillBar.value = _minFillValue;
        _fillBar.interactable = false;
    }

    public void SetMaxValue(int value, bool saveFillPercent = false)
    {
        if (saveFillPercent)
        {
            float percent = (float)(_value - _minFillValue) / (_maxFillValue - _minFillValue);
        }

        _fillBar.maxValue = value;
        _maxFillValue = value;
    }

    public void SetMinValue(int value, bool saveFillPercent = false)
    {
        _fillBar.minValue = value;
        _minFillValue = value;

        if (saveFillPercent)
        {

        }
    }

    protected virtual void UpdateBar()
    {
        _fillBar.value = _value;
    }
}
