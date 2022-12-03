using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : FillBar
{
    [SerializeField] private Text _loadingValueText;

    public void SetValue(int value)
    {
        _value = value;

        _loadingValueText.text = value.ToString() + "%";

        UpdateBar();
    }
}
