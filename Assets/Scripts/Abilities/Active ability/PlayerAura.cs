using UnityEngine;

public sealed class PlayerAura : ColliderWeapon
{
    [SerializeField] private ParticleSystem _particle;

    public override bool Upgrade(Upgrade upgrade)
    {
        bool isLevelUp = base.Upgrade(upgrade);

        _particle.transform.localScale = new Vector3(_stats.AttackRange.Value, _stats.AttackRange.Value, _stats.AttackRange.Value);

        return isLevelUp;
    }
}
