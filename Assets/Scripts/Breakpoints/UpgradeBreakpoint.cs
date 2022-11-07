using UnityEngine;

[System.Serializable]
public class UpgradeBreakpoint : Breakpoint
{
    [Header("Upgrade settings")]
    [SerializeField] private Upgrade _upgrade;

    public Upgrade Upgrade => _upgrade;
}
