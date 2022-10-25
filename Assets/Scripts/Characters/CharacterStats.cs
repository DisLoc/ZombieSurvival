using UnityEngine;

[System.Serializable]
public class CharacterStats : IObjectStats
{
    [SerializeField] protected Weapon _baseWeapon;
    [SerializeField] protected Health _health;
    [SerializeField] protected MoveSpeed _velocity;
    [SerializeField] protected Regeneration _regeneration;

    public Weapon BaseWeapon => _baseWeapon;
    public Health Health => _health;
    public MoveSpeed Velocity => _velocity;
    public Regeneration Regeneration => _regeneration;

    public virtual void Initialize()
    {
        _health.Initialize();
        _velocity.Initialize();
        _regeneration.Initialize();
    }

    public virtual void GetUpgrade(Upgrade upgrade)
    {
        _health.Upgrade(upgrade);
        _velocity.Upgrade(upgrade);
        _regeneration.Upgrade(upgrade);
    }
}
