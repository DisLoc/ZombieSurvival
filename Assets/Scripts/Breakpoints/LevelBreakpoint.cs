using UnityEngine;

[System.Serializable]
public class LevelBreakpoint : Breakpoint
{
    [Tooltip("Time in seconds")]
    [SerializeField] private int _requiredTime;
    [SerializeField] private LevelReward _reward;

    [HideInInspector] public bool wasClaimed;

    /// <summary>
    /// Time in seconds
    /// </summary>
    public int RequiredTime => _requiredTime;
    public LevelReward Reward => _reward;

    protected LevelBreakpoint(LevelBreakpoint breakpoint) : base(breakpoint)
    {
        _reward = breakpoint._reward;
    }

    public override Breakpoint Clone() => new LevelBreakpoint(this);
}
