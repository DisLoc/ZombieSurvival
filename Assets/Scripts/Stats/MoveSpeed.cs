using UnityEngine;

[System.Serializable]
public struct MoveSpeed : IStat
{
    [SerializeField] private float _moveSpeed;
    public float Value => _moveSpeed;

    [SerializeField] private Level _level;
    public Level Lvl => _level;

    public void Upgrade(Upgrade upgrade)
    {
        _level.LevelUp();
    }
}
