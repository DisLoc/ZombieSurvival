using UnityEngine;

[System.Serializable]
public struct HealthPoint : IStat
{
    [SerializeField] private float _maxHP;
    private float _hp;

    public float Value => _hp;
    public float MaxHP => _maxHP;

    [SerializeField] private Level _level;
    public Level Lvl => _level;

    public void Initialize()
    {
        _hp = _maxHP;
    }

    public void Upgrade(Upgrade upgrade)
    {

    }

    public void TakeDamage(int damage)
    {
        _hp -= damage;
    }

    public void Heal(int heal)
    {
        _hp += heal;

        if (_hp > _maxHP) _hp = _maxHP;
    }
}
