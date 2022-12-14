using UnityEngine;

[System.Serializable]
public class BossBreakpoint : Breakpoint
{
    [Header("Boss settings")]
    [SerializeField] protected Enemy _bossPrefab;
    [SerializeField] protected Upgrade _bossUpgrade;

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
    public Upgrade BossUpgrade => _bossUpgrade;

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
        _bossUpgrade = breakpoint._bossUpgrade;
        
        _isFinalBoss = breakpoint._isFinalBoss;
        _maxAbilitiesRewardCount = breakpoint._maxAbilitiesRewardCount;

        _hasRandomMaterialReward = breakpoint._hasRandomMaterialReward;
        _specificMaterialReward = breakpoint._specificMaterialReward;
        _materialsCount = breakpoint._materialsCount;

        _hasRandomEquipmentReward = breakpoint._hasRandomEquipmentReward;
        _specificRandomEquipmentRarity = breakpoint._specificRandomEquipmentRarity;
        _specificEquipmentReward = breakpoint._specificEquipmentReward;
    }

    public override Breakpoint Clone() => new BossBreakpoint(this);
}
