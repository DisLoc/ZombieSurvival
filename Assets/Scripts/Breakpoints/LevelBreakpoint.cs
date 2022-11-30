using UnityEngine;

public class LevelBreakpoint : Breakpoint
{
    [Tooltip("Time in seconds")]
    [SerializeField] private int _requiredTime;
    [SerializeField] private Reward _reward;

    [HideInInspector] public bool wasClaimed;

    public int RequiredTime => _requiredTime;
    public Reward Reward => _reward;

    protected LevelBreakpoint(LevelBreakpoint breakpoint) : base(breakpoint)
    {
        _reward = breakpoint._reward;
    }

    public override Breakpoint Clone() => new LevelBreakpoint(this);
}
