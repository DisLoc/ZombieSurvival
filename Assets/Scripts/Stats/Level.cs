using UnityEngine;

[System.Serializable]
public struct Level
{
    [SerializeField] private int _maxLevel;
    private int _level;

    public int Value => _level;
    public Level Lvl => this;

    public void LevelUp()
    {

    }
}
