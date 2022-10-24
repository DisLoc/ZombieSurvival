using UnityEngine;

[System.Serializable]
public sealed class PlayerStats : CharacterStats
{
    [SerializeField] private PlayerExpLevel _expLevel;
    [SerializeField] private PickUpRange _pickUpRange;

    public PickUpRange PickUpRange => _pickUpRange;
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

    /// <summary>
    /// Add expirience to player
    /// </summary>
    /// <param name="exp">Value need to add</param>
    public void AddExpirience(int exp)
    {
        _expLevel.AddExp(exp);
    }
}
