using UnityEngine;

public class Aura : ColliderWeapon
{
    [SerializeField] protected ParticleSystem _particle;

    public override bool Upgrade(Upgrade upgrade)
    {
        bool isLevelUp = base.Upgrade(upgrade);

        if (_particle != null)
        {
            _particle.transform.localScale = new Vector3(_stats.AttackRange.Value, _stats.AttackRange.Value, _stats.AttackRange.Value);
        }
        else
        {
            transform.localScale = new Vector3(_stats.AttackRange.Value, _stats.AttackRange.Value, _stats.AttackRange.Value);
        }

        return isLevelUp;
    }
}
