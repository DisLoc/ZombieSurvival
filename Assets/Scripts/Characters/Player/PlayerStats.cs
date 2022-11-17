using UnityEngine;

[System.Serializable]
public sealed class PlayerStats : CharacterStats
{
    [SerializeField] private Damage _damage;
    [SerializeField] private PlayerExpLevel _expLevel;
    [SerializeField] private PickUpRange _pickUpRange;
    [SerializeField] private ReanimationCount _rebornCount;

    [Header("LevelUp settings")]
    [SerializeField] private AbilityChooseCount _abilityChooseCount;
    [SerializeField] private AbilitiesPerChoice _abilitiesPerChoice;
    [SerializeField] private AbilitiesRerollCount _abilitiesRerollCount;

    public Damage Damage => _damage;
    public PickUpRange PickUpRange => _pickUpRange;
    public int Exp => (int)_expLevel.Exp.Value;
    public int Level => (int)_expLevel.Value;
    public float LevelProgress => _expLevel.LevelProgress;

    public ReanimationCount RebornCount => _rebornCount;
    public AbilityChooseCount AbilityChooseCount => _abilityChooseCount;
    public AbilitiesPerChoice AbilitiesPerChoice => _abilitiesPerChoice;
    public AbilitiesRerollCount AbilitiiesRerollCount => _abilitiesRerollCount;

    public override void Initialize()
    {
        base.Initialize();

        _expLevel.Initialize();
        _pickUpRange.Initialize();
        _rebornCount.Initialize();

        _abilityChooseCount.Initialize();
        _abilitiesPerChoice.Initialize();
        _abilitiesRerollCount.Initialize();
    }

    public override void GetUpgrade(Upgrade upgrade)
    {
        base.GetUpgrade(upgrade);

        _expLevel.Upgrade(upgrade);
        _pickUpRange.Upgrade(upgrade);
        _rebornCount.Upgrade(upgrade);

        _abilityChooseCount.Upgrade(upgrade);
        _abilitiesPerChoice.Upgrade(upgrade);
        _abilitiesRerollCount.Upgrade(upgrade);
    }

    public override void DispelUpgrade(Upgrade upgrade)
    {
        base.DispelUpgrade(upgrade);

        _expLevel.DispelUpgrade(upgrade);
        _pickUpRange.DispelUpgrade(upgrade);
        _rebornCount.DispelUpgrade(upgrade);

        _abilityChooseCount.DispelUpgrade(upgrade);
        _abilitiesPerChoice.DispelUpgrade(upgrade);
        _abilitiesRerollCount.DispelUpgrade(upgrade);
    }

    /// <summary>
    /// Add expirience to player
    /// </summary>
    /// <param name="exp">Value need to add</param>
    public void AddExpirience(int exp)
    {
        _expLevel.AddExp(exp);
    }
}
