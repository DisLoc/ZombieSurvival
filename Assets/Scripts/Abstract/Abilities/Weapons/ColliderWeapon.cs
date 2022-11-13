using System.Collections.Generic;
using UnityEngine;

public abstract class ColliderWeapon : Weapon
{
    [SerializeField] protected WeaponAbilityStats _stats;

    public override AbilityStats Stats => _stats;

    public override void Attack()
    {
        if (_isReady)
        {
            base.Attack();

            List<GameObject> targets = _targetDetector.Targets;

            if (targets.Count == 0) return;

            for (int i = 0; i < targets.Count; i++)
            {
                DamageableObject target = targets[i].GetComponent<DamageableObject>();

                if (target != null)
                {
                    target.TakeDamage((int)_stats.Damage.Value);
                }
            }

            _isReady = false;
            _attackIntervalTimer = _stats.AttackInterval.Value;
        }
    }

    public override bool Upgrade(Upgrade upgrade)
    {
        _stats.GetUpgrade(upgrade);

        return base.Upgrade(upgrade);
    }
}
