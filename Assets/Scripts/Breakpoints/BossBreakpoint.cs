using UnityEngine;

[System.Serializable]
public class BossBreakpoint : Breakpoint
{
    [Header("Boss settings")]
    [SerializeField] protected Enemy _bossPrefab;

    public Enemy BossPrefab => _bossPrefab;

    protected BossBreakpoint (BossBreakpoint breakpoint) : base (breakpoint)
    {
        _bossPrefab = breakpoint._bossPrefab;
    }

    public override Breakpoint Clone() => new BossBreakpoint(this);
}
