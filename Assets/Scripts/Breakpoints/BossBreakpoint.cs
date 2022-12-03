using UnityEngine;

[System.Serializable]
public class BossBreakpoint : Breakpoint
{
    [Header("Boss settings")]
    [SerializeField] protected Enemy _bossPrefab;

    [Tooltip("If that enemy is final Boss, it has not abilities and supplies rewards")]
    [SerializeField] private bool _isFinalBoss;

    [SerializeField] private int _maxAbilitiesRewardCount;

    [SerializeField] private bool _hasRandomMaterialReward;
    [Tooltip("If HasRandomMaterialReward is true, field can be null")]
    [SerializeField] private EquipmentMaterial _specificMaterialReward;
    [SerializeField] private int _materialsCount;

    [Header("Final Boss rewards (fill next field if IsFinalBoss is true)")]
    [SerializeField] private bool _hasRandomEquipmentReward;
    [Tooltip("If HasRandomEquipmentReward is true, must fill that field")]
    [SerializeField] private EquipRarity _specificRandomEquipmentRarity;
    [Tooltip("If HasRandomEquipmentReward is true, field can be null")]
    [SerializeField] private Equipment _specificEquipmentReward;

    public Enemy BossPrefab => _bossPrefab;

    public bool IsFinalBoss => _isFinalBoss;
    public int MaxAbilitiesRewardCount => _maxAbilitiesRewardCount;

    public bool HasRandomEquipmentReward => _hasRandomEquipmentReward;
    public EquipRarity RandomEquipmentRarity => _specificRandomEquipmentRarity;
    public Equipment SpecificEquipmentReward => _specificEquipmentReward;

    public bool HasRandomMaterialReward => _hasRandomMaterialReward;
    public EquipmentMaterial SpecificMaterialReward => _specificMaterialReward;
    public int MaterialsCount => _materialsCount;

    protected BossBreakpoint (BossBreakpoint breakpoint) : base (breakpoint)
    {
        _bossPrefab = breakpoint._bossPrefab;
    }

    public override Breakpoint Clone() => new BossBreakpoint(this);
}
