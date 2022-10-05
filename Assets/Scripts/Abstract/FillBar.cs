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

    public virtual void Initialize()
    {
        _fillBar.minValue = _minFillValue;
        _fillBar.maxValue = _maxFillValue;

        _fillBar.interactable = false;

        UpdateBar();
    }

    protected virtual void UpdateBar()
    {
        _fillBar.value = _value;
    }
}
