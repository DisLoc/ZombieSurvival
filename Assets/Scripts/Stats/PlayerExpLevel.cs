using UnityEngine;
using Zenject;

[System.Serializable]
public class PlayerExpLevel : Stat
{
    [SerializeField] protected Expirience _exp;

    [Header("LevelUp settings")]
    [SerializeField] protected SoundList _sounds;
    [SerializeField] protected int _baseExpForLevelUp;
    [Tooltip("ExpForLevel = BaseExpForLevel * LevelMultiplier * CurrentLevel")]
    [SerializeField] protected float _levelMultiplier;
    [SerializeField] protected PlayerExpBar _expBar;
    
    public Expirience Exp => _exp;
    /// <summary>
    /// Expirience value required for level up
    /// </summary>
    public int ExpForLevel => (int)(_baseExpForLevelUp * _levelMultiplier * _value);
    /// <summary>
    /// Can be in range [0, 1]
    /// </summary>
    public float LevelProgress => _exp.Value / ExpForLevel;

    public override void Initialize()
    {
        base.Initialize();

        _exp.SetValue(0);

        _expBar.Initialize(this);
    }

    public PlayerExpLevel(StatData statData, int baseExpForLevel, float levelMuliplier, ExpirienceData expirienceData, PlayerExpBar expBar = null,
                          UpgradeList upgradeList = null, bool isDebug = false) : base(statData, upgradeList, isDebug)
    {
        _baseExpForLevelUp = baseExpForLevel;
        _levelMultiplier = levelMuliplier;

        _exp = new Expirience(expirienceData);
        _expBar = expBar;
    }

    /// <summary>
    /// Add expirience to player
    /// </summary>
    /// <param name="exp">Value need to add</param>
    public void AddExp(int exp)
    {
        if (_isDebug) Debug.Log("Add " + exp + " expirience. Total: " + (exp + _upgrades.UpgradesValue) * _upgrades.UpgradesMultiplier);
             
        _exp.Add((exp + _upgrades.UpgradesValue) * _upgrades.UpgradesMultiplier);

        if (LevelProgress >= 1)
        {
            _exp.SetValue(0);
            _value++;

            if (_expBar != null)
            {
                _expBar.UpdateLevel();
            }
            else if (_isDebug) Debug.Log("Missing ExpBar!");

            _sounds.PlaySound(SoundTypes.LevelUp);

            EventBus.Publish<IPlayerLevelUpHandler>(handler => handler.OnPlayerLevelUp());
        }

        if (_expBar != null)
        {
            _expBar.UpdateExp();
        }
        else if (_isDebug) Debug.Log("Missing ExpBar!");
    }

    #region Serialization
    public SerializableData SaveData()
    {
        ExpLevelData data = new ExpLevelData();

        data.level = (int)_value;
        data.expirience = (int)_exp.Value;

        return data;
    } 

    public void LoadData(SerializableData data) 
    {
        if (data == null) return;

        if (data is ExpLevelData levelData)
        {
            SetValue(levelData.level);
            _exp.SetValue(levelData.expirience);

            if (_expBar != null)
            {
                _expBar.UpdateLevel();
                _expBar.UpdateExp();
            }
            else if (_isDebug) Debug.Log("Missing ExpBar!");
        }
    }

    public void ResetData()
    {
        _exp.SetValue();
        _value = BaseValue;

        if (_expBar != null)
        {
            _expBar.UpdateLevel();
            _expBar.UpdateExp();
        }
        else if (_isDebug) Debug.Log("Missing ExpBar!");
    }

    [System.Serializable]
    private class ExpLevelData : SerializableData
    {
        public int level;
        public int expirience;
    }
    #endregion
}
