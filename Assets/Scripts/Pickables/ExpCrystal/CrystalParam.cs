using UnityEngine;

[System.Serializable]
public class CrystalParam
{
    [Tooltip("Value that character get when take crystal")]
    [SerializeField] private int _expValue;
    [Tooltip("Crystal color")]
    [SerializeField] private Color _color;

    public int ExpValue => _expValue;
    public Color Color => _color;
}