using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelRewardBreakpoints
{
    [SerializeField] private LevelBreakpoint _breakpoint1;
    [SerializeField] private LevelBreakpoint _breakpoint2;
    [SerializeField] private LevelBreakpoint _breakpoint3;

    public int ReachedProgress
    {
        get
        {
            int progress = 0;
            foreach (Breakpoint breakpoint in Breakpoints)
            {
                if (breakpoint.IsReached) progress = breakpoint.RequiredProgress;

                else break;
            }

            return progress;
        }
    }
    public List<LevelBreakpoint> Breakpoints => new List<LevelBreakpoint>() { _breakpoint1, _breakpoint2, _breakpoint3 };

    public Breakpoint CheckReaching(int progress)
    {
        foreach (Breakpoint breakpoint in Breakpoints)
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
