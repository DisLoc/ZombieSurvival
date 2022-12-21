using UnityEngine;

[System.Serializable]
public class CampUpgrade 
{
    [SerializeField] private Sprite _upgradeIcon;
    [SerializeField] private Level _level;
    [SerializeField] private Upgrade _repeatingUpgrade;
    [Tooltip("Field can be null")]
    [SerializeField] private CampUpgradeButton _button;

    public Sprite Icon => _upgradeIcon;
    public Level Level => _level;
    public Upgrade RepeatingUpgrade => _repeatingUpgrade;
    public Upgrade CurrentUpgrade => (int)_level.Value > 0 ? _repeatingUpgrade * (int)_level.Value : null;

    public void Initialize()
    {
        _level.Initialize();

        _button?.Initialize(this);
        UpdateValues();
    }

    public void Upgrade()
    {
        _level.LevelUp();

        UpdateValues();
    }

    public void UpdateValues()
    {
        _button?.UpdateValues();
    }
}
