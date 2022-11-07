using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Level/Crystal spawning stats", fileName = "New crystal stats")]
public class CrystalStats : ScriptableObject
{
    [Header("Settings")]
    [SerializeField] private ExpCrystal _crystalPrefab;
    [SerializeField] private List<ObjectChanceSpawn<CrystalParam>> _crystalSpawnParams;
    
    public ExpCrystal CrystalPrefab => _crystalPrefab;
    public List<ObjectChanceSpawn<CrystalParam>> CrystalSpawnParams => _crystalSpawnParams;
}
