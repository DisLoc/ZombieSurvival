using UnityEngine;

[System.Serializable]
public class BossBreakpoint : Breakpoint
{
    [Header("Boss settings")]
    [SerializeField] protected Enemy _bossPrefab;
    [SerializeField] protected GameObject _bossEventFence;

    public Enemy BossPrefab => _bossPrefab;
    public GameObject BossEventFence => _bossEventFence;

    protected BossBreakpoint (BossBreakpoint breakpoint) : base (breakpoint)
    {
        _bossPrefab = breakpoint._bossPrefab;
        _bossEventFence = breakpoint._bossEventFence;
    }

    public override Breakpoint Clone() => new BossBreakpoint(this);
}
