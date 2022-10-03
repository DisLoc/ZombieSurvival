using UnityEngine;

[System.Serializable]
public struct ProjectileNumber : IStat
{
    [SerializeField] private int _projectileNumber;
    [SerializeField] private Level _level;

    public float Value => _projectileNumber;
    public Level Lvl => _level;

    public void Upgrade(Upgrade upgrade)
    {

    }
}
