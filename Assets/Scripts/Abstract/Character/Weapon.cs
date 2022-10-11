using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    protected bool _isReady;

    public virtual Ability Ability { get; }

    public virtual void Initialize()
    {
        
    }

    public virtual void Attack(DamageableObject target)
    {
        if (_isDebug) Debug.Log(name + " attacks " + target.name);
    }

    protected virtual IEnumerator WaitReload()
    {
        _isReady = false;
        yield return new WaitForSeconds(0);
        _isReady = true;

        if (_isDebug) Debug.Log(name + " can attack");
    }

    public void UpdateTimer()
    {

    }
}
