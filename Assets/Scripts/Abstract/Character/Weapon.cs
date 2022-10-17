using UnityEngine;

public abstract class Weapon : AbilityContainer, IUpdatable
{
    [Header("Ability settings")]
    [SerializeField] protected TargetDetector _targetDetector;
    [SerializeField] protected WeaponAbilityUpgradeData _abilityUpgradeData;
 
    protected bool _isReady;

    public override AbilityUpgradeData UpgradeData => _abilityUpgradeData;
    public override CurrentUpgrade CurrentUpgrade => _abilityUpgradeData.Upgrades[(int)Stats.Level.Value - 1];

    public override void Initialize()
    {
        base.Initialize();

        _targetDetector.Initialize();
    }

    public virtual void Attack()
    {
        if (_isDebug) Debug.Log(name + " attacks");
    }

    public virtual void OnUpdate()
    {

    }
}
