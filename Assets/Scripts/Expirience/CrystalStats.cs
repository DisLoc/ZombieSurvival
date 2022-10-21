using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Expirience/Crystal stats", fileName = "New crystal stats")]
public class CrystalStats : ScriptableObject
{
    [Header("Settings")]
    [SerializeField] private ExpCrystal _crystalPrefab;
    [SerializeField] private List<CrystalParam> _crystalParams;

    public ExpCrystal CrystalPrefab => _crystalPrefab;
    public List<CrystalParam> CrystalParams => _crystalParams;
}
