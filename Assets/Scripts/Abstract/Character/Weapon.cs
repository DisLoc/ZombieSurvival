using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    [Header("Stats settings")]
    [SerializeField] protected WeaponStats _stats;
    public WeaponStats Stats => _stats;
   
    protected bool _isReady;

    public virtual void Initialize()
    {
        _stats.Initialize();
    }

    public virtual void Attack(DamageableObject target)
    {
        if (_isDebug) Debug.Log(name + " attacks " + target.name);
    }

    protected virtual IEnumerator WaitReload()
    {
        _isReady = false;
        yield return new WaitForSeconds(_stats.AttackInterval.Value);
        _isReady = true;

        if (_isDebug) Debug.Log(name + " can attack");
    }

    public void UpdateTimer()
    {

    }
}
