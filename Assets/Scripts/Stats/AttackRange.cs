using UnityEngine;

[System.Serializable]
public struct AttackRange : IStat
{
    [SerializeField] private float _attackRange;
    public float Value => _attackRange;

    [SerializeField] private Level _level;
    public Level Lvl => _level;

    public void Upgrade(Upgrade upgrade)
    {
        _level.LevelUp();
    }
}
