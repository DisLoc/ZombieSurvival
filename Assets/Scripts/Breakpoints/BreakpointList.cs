using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BreakpointList<T> where T : Breakpoint
{
    [SerializeField] private List<T> _breakpoints;

    public List<T> Breakpoints => _breakpoints;

    public Breakpoint CheckReaching(int progress)
    {
        foreach(Breakpoint breakpoint in _breakpoints)
        {
            if (breakpoint.IsReached) continue;

            else if (breakpoint.RequiredProgress <= progress)
            {
                return breakpoint;
            }
        }
        return null;
    }
}
