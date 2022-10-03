using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    [Header("Weapon settings")]
    [SerializeField] protected Damage _baseDamage;
    [SerializeField] protected AttackRange _attackRange;
    [SerializeField] protected Cooldown _attackCooldown;

    [SerializeField] protected Tags _targetTag;

    protected DamageableObject _target;
    protected bool _isReady;

    public int Damage => (int)_baseDamage.Value;
    public float Cooldown => _attackCooldown.Value;
    public string TargetTag => _targetTag.ToString();

    public virtual void Initialize()
    {

    }

    public abstract void Attack();

    protected IEnumerator WaitReload()
    {
        _isReady = false;
        yield return new WaitForSeconds(Cooldown);
        _isReady = true;
    }
}
