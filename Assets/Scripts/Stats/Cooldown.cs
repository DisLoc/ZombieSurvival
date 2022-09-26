using UnityEngine;

[System.Serializable]
public struct Cooldown : IStat
{
    [SerializeField] private float _cooldown;
    public float Value => _cooldown;

    [SerializeField] private Level _level;
    public Level Lvl => _level;

    public void Upgrade(Upgrade upgrade)
    {

    }
}
