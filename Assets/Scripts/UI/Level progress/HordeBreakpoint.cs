using UnityEngine;

[System.Serializable]
public class HordeBreakpoint : Breakpoint
{
    [SerializeField] protected Zombie _zombie;

    public Zombie Zombie => _zombie;
}
