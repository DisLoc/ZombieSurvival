using UnityEngine;

[System.Serializable]
public struct ProjectileSpeed : IStat
{
    [SerializeField] private float _projectileSpeed;
    public float Value => _projectileSpeed;

    [SerializeField] private Level _level;
    public Level Lvl => _level; 

    public void Upgrade(Upgrade upgrade)
    {
        _level.LevelUp();
    }
}
