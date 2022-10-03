using UnityEngine;

[System.Serializable]
public class ExpLevel : IStat
{
    [SerializeField] private int _expForLevel;
    [SerializeField] private int _expLevelMultiplier;
    [SerializeField] private Level _level;

    private Expirience _expirience = new Expirience();

    public Level Lvl => _level;
    public float Value => _expirience.Value;

    public void Upgrade(Upgrade upgrade)
    {
        _level.LevelUp();
    }
}
