using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BreakpointList<T> where T : Breakpoint
{
    [SerializeField] private List<T> _breakpoints;

    public List<T> Breakpoints => _breakpoints;

    public BreakpointList()
    {
        _breakpoints = new List<T>();
    }

    public BreakpointList(List<T> breakpoints)
    {
        _breakpoints = breakpoints;
    }

    public void Add(T breakpoint)
    {
        _breakpoints.Add(breakpoint);
    }

    public Breakpoint CheckReaching(int progress)
    {
        foreach(Breakpoint breakpoint in _breakpoints)
        {
            if (breakpoint.IsReached) continue;

            else if (breakpoint.RequiredProgress <= progress)
            {
                breakpoint.SetReached(true);

                return breakpoint;
            }
        }
        return null;
    }
}
