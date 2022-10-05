using UnityEngine;

[System.Serializable]
public class ExpLevel : Stat
{
    [SerializeField] protected Expirience _expLevel;

    public float LevelProgress { get; }

    public void AddExp(int exp)
    {
        _value += (exp + _upgrades.UpgradesValue) * _upgrades.UpgradesMultiplier;
    }
}
