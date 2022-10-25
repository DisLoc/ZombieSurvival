using UnityEngine;

public abstract class Weapon : AbilityContainer, IUpdatable
{
    [Header("Ability settings")]
    [SerializeField] protected TargetDetector _targetDetector;
    [SerializeField] protected WeaponAbilityUpgradeData _abilityUpgradeData;

    protected float _attackIntervalTimer;
    protected bool _isReady;

    public override AbilityUpgradeData UpgradeData => _abilityUpgradeData;
    public override CurrentUpgrade CurrentUpgrade => _abilityUpgradeData.Upgrades[(int)Stats.Level.Value];

    public override void Initialize()
    {
        base.Initialize();

        _isReady = true;

        _targetDetector.Initialize((Stats as WeaponAbilityStats).AttackRange);
    }

    /// <summary>
    /// Attack target if ready based on self rules
    /// </summary>
    public virtual void Attack() // need override
    {
        if (_isDebug) Debug.Log(name + " attacks");
    }

    /// <summary>
    /// Update cooldown timer
    /// </summary>
    public virtual void OnUpdate()
    {
        if (_isReady) return;

        _attackIntervalTimer -= Time.deltaTime;

        if (_attackIntervalTimer <= 0f)
        {
            _isReady = true;
        } 

    }

    public virtual void UpgradeToSuper(Weapon super)
    {

    }
}
