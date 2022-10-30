using UnityEngine;

[System.Serializable]
public class BossBreakpoint : Breakpoint
{
    [Header("Boss settings")]
    [SerializeField] protected Zombie _bossPrefab;
    [SerializeField] protected GameObject _bossEventFence;

    public Zombie BossPrefab => _bossPrefab;
    public GameObject BossEventFence => _bossEventFence;
}
