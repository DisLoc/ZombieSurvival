using UnityEngine;

[System.Serializable]
public sealed class PlayerStats : CharacterStats
{
    [SerializeField] private ExpLevel _expirience;
    [SerializeField] private PickUpRange _pickUpRange;

    public float PickUpRange => _pickUpRange.Value;
    public int Exp => (int)_expirience.Value;
    public int Level => (int)_expirience.Level.Value;
    public float LevelProgress => _expirience.LevelProgress; 

    public override void Initialize()
    {
        base.Initialize();
        _expirience.Initialize();
        _pickUpRange.Initialize();
    }
}
