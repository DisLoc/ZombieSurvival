using UnityEngine;

[System.Serializable]
public class CharacterStats : IObjectStats
{
    [SerializeField] protected Weapon _baseWeapon;
    [SerializeField] protected HealthPoint _healthPoints;
    [SerializeField] protected MoveSpeed _velocity;
    [SerializeField] protected Regeneration _regeneration;

    public HealthPoint HP => _healthPoints;
    public Weapon BaseWeapon => _baseWeapon;
    public float Velocity => _velocity.Value;
    public float Regeneration => _regeneration.Value;

    public virtual void Initialize()
    {
        _healthPoints.Initialize();
        _velocity.Initialize();
        _regeneration.Initialize();
    }

    public virtual void GetUpgrade(Upgrade upgrade)
    {
        _healthPoints.Upgrade(upgrade);
        _velocity.Upgrade(upgrade);
        _regeneration.Upgrade(upgrade);
    }
}
