using UnityEngine;

[System.Serializable]
public struct Damage : IStat
{
    [SerializeField] private float _damage;
    public float Value => _damage;

    [SerializeField] private Level _level;
    public Level Lvl => _level;

    public void Upgrade(Upgrade upgrade)
    {

    }
}
