using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Stats/StatData", fileName = "New StatData")]
public class StatData : ScriptableObject
{
    [Header("Value settings")]
    [SerializeField] private float _baseValue;
    [SerializeField] private float _minValue;
    [SerializeField] private float _maxValue;
    [SerializeField] private bool _maxValueIsInfinite;

    [Header("Upgrade settings")]
    [SerializeField] private MarkerList _upgradeMarkers;

    public float BaseValue => _baseValue;
    public float MinValue => _minValue;
    public float MaxValue => _maxValue;
    public bool MaxValueIsInfinite => _maxValueIsInfinite;
    public MarkerList Markers => _upgradeMarkers;
}
