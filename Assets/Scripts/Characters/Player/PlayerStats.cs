using UnityEngine;

[System.Serializable]
public sealed class PlayerStats : CharacterStats
{
    [SerializeField] private float _pickUpRange;
    [SerializeField] private ExpLevel _expirience;

   // public int Exp => _expirience.Exp;
  //  public int Level => _expirience.Level;
   // public float LevelProgress => _expirience.LevelProgress;
}
