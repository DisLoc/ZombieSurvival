using UnityEngine;

public abstract class AbilityUpgradeData : ScriptableObject
{
    [SerializeField] protected string _upgradeName;

    public string UpgradeName => _upgradeName;
}
