using UnityEngine;

[System.Serializable]
public sealed class PlayerStats : CharacterStats
{
    [SerializeField] private ExpLevel _expLevel;
    [SerializeField] private PickUpRange _pickUpRange;

    public float PickUpRange => _pickUpRange.Value;
    public int Exp => (int)_expLevel.Exp.Value;
    public int Level => (int)_expLevel.Value;
    public float LevelProgress => _expLevel.LevelProgress; 

    public override void Initialize()
    {
        base.Initialize();
        _expLevel.Initialize();
        _pickUpRange.Initialize();
    }

    public override void GetUpgrade(Upgrade upgrade)
    {
        base.GetUpgrade(upgrade);

        _expLevel.Upgrade(upgrade);
        _pickUpRange.Upgrade(upgrade);
    }
}
