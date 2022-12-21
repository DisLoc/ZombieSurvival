using UnityEngine;

[System.Serializable]
public class CampUpgradeLimit
{
    [SerializeField] private int _requiredPlayerLevel;
    [SerializeField] private int _maxCampLevel;

    public int RequiredPlayerLevel => _requiredPlayerLevel;
    public int MaxCampLevel => _maxCampLevel;
}
