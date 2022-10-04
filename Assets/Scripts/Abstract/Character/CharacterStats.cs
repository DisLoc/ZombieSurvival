using UnityEngine;

[System.Serializable]
public class CharacterStats
{
    [SerializeField] protected Weapon _weapon;
    [SerializeField] protected HealthPoint _healthPoints;
    [SerializeField] protected MoveSpeed _velocity;

    public HealthPoint HP => _healthPoints;
    public Weapon Weapon => _weapon;
    public float Velocity => _velocity.Value;

    public virtual void Initialize()
    {
        _healthPoints.Initialize();
        _velocity.Initialize();
    }
}
