using UnityEngine;

[System.Serializable]
public struct Cooldown : IStat
{
    [SerializeField] private float _cooldown;
    [SerializeField] private Level _level;

    public float Value => _cooldown;
    public Level Lvl => _level;

    public void Upgrade(Upgrade upgrade)
    {

    }
}
