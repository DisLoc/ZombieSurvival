using UnityEngine;

[System.Serializable]
public class ExpLevel
{
    [SerializeField] private int _expForLevel;
    [SerializeField] private int _expLevelMultiplier;
    [SerializeField] private Level _level;

    private Expirience _expirience = new Expirience();

    public Level Lvl => _level;
    public int Value => _expirience.Value;

    public void Upgrade(Upgrade upgrade)
    {

    }
}
