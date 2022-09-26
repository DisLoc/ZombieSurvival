using UnityEngine;

[System.Serializable]
public struct Level
{
    [SerializeField] private int _maxLevel;
    private int _level;

    public int Lvl => _level;

    public void LevelUp()
    {
        _level++;
    }
}
