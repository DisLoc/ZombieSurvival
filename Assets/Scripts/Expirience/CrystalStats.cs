using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Expirience/Crystal stats", fileName = "New crystal stats")]
public class CrystalStats : ScriptableObject
{
    [Header("Settings")]
    [SerializeField] private ExpCrystal _crystalPrefab;
    [SerializeField] private List<CrystalParam> _crystalParams;

    public ExpCrystal CrystalPrefab => _crystalPrefab;
    public List<CrystalParam> CrystalParams => _crystalParams;
}

[System.Serializable]
public class CrystalParam
{
    [SerializeField] private int _expValue;
    [SerializeField] private Color _color;

    public int ExpValue => _expValue;
    public Color Color => _color;
}