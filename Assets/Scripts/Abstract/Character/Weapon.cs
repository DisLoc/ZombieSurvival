using UnityEngine;

public abstract class Weapon : AbilityContainer, IUpdatable
{
    [Header("Ability settings")]
    [SerializeField] protected TargetDetector _targetDetector;
    [SerializeField] protected WeaponAbilityUpgradeData _abilityUpgradeData;
 
    protected bool _isReady;

    public override AbilityUpgradeData UpgradeData => _abilityUpgradeData;

    public virtual void Attack()
    {
        if (_isDebug) Debug.Log(name + " attacks");
    }

    public virtual void OnUpdate()
    {

    }
}
