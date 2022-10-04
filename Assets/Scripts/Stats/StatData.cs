using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Stats/StatData", fileName = "New StatData")]
public class StatData : ScriptableObject
{
    [Header("Value settings")]
    [SerializeField] private float _baseValue;
    [Tooltip("Set -1 for infinite value")]
    [SerializeField] private float _maxValue;

    [Header("Upgrade settings")]
    [SerializeField] private StatMarker _upgradeMarker;
    [SerializeField] private Level _level;

    public float BaseValue => _baseValue;
    public float MaxValue => _maxValue;
    public Level Level => _level;
    public StatMarker Marker => _upgradeMarker;
}
