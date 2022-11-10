using UnityEngine;

[System.Serializable]
public class UpgradeBreakpoint : Breakpoint
{
    [Header("Upgrade settings")]
    [SerializeField] private Upgrade _upgrade;

    public Upgrade Upgrade => _upgrade;

    protected UpgradeBreakpoint(UpgradeBreakpoint breakpoint) : base(breakpoint)
    {
        _upgrade = breakpoint._upgrade;
    }

    public override Breakpoint Clone() => new UpgradeBreakpoint(this);
}
