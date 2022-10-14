using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    protected bool _isReady;

    public virtual AbilityData Ability { get; }

    public virtual void Initialize()
    {
        
    }

    public virtual void Attack()
    {
        if (_isDebug) Debug.Log(name + " attacks");
    }

    public void UpdateTimer()
    {

    }
}
