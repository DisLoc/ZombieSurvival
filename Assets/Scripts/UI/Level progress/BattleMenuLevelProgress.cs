public class BattleMenuLevelProgress : FillBar
{
    public void Initialize(LevelRewardBreakpoints breakpoints)
    {
        if (breakpoints == null) return;

        _minFillValue = breakpoints.Breakpoints[0].RequiredProgress;
        _maxFillValue = breakpoints.Breakpoints[2].RequiredProgress;

        _value = breakpoints.ReachedProgress;

        Initialize();
    }
}
