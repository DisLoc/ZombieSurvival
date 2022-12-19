using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Upgrades/Upgrade", fileName = "New upgrade")]
public class Upgrade : ScriptableObject
{
    [SerializeField] private bool _isAbilityUpgrade;
    [SerializeField] private AbilityMarker _abilityMarker;

    [SerializeField] private List<UpgradeData> _upgrades;

    public bool IsAbilityUpgrade => _isAbilityUpgrade;
    public AbilityMarker AbilityMarker => _abilityMarker;
    public List<UpgradeData> Upgrades => _upgrades;

    public Upgrade (List<UpgradeData> data, AbilityMarker abilityMarker = null, bool isAbilityUpgrade = false)
    {
        _upgrades = data;
        _abilityMarker = abilityMarker;
        _isAbilityUpgrade = isAbilityUpgrade;
    }

    public Upgrade (UpgradeData data, AbilityMarker abilityMarker = null, bool isAbilityUpgrade = false)
    {
        _upgrades = new List<UpgradeData>();
        _upgrades.Add(data);

        _abilityMarker = abilityMarker;
        _isAbilityUpgrade = isAbilityUpgrade;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="first"></param>
    /// <param name="other"></param>
    /// <returns>Returns new upgrade only with data (markers will not be saved)</returns>
    public static Upgrade operator +(Upgrade first, Upgrade other)
    {
        List<UpgradeData> data = new List<UpgradeData> ();

        data.AddRange(first.Upgrades);
        data.AddRange(other.Upgrades);

        return new Upgrade(data);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="upgrade"></param>
    /// <param name="data"></param>
    /// <returns>Returns new upgrade with additional data</returns>
    public static Upgrade operator +(Upgrade upgrade, UpgradeData data)
    {
        List<UpgradeData> datas = new List<UpgradeData>();

        datas.AddRange(upgrade.Upgrades);
        if (data != null)
        {
            datas.Add(data);
        }

        return new Upgrade(datas, upgrade._abilityMarker, upgrade._isAbilityUpgrade);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="upgrade"></param>
    /// <param name="data"></param>
    /// <returns>Returns new upgrade with additional data</returns>
    public static Upgrade operator +(Upgrade upgrade, List<UpgradeData> data)
    {
        List<UpgradeData> datas = new List<UpgradeData>();

        datas.AddRange(upgrade.Upgrades);
        
        if (data != null && data.Count > 0)
        {
            datas.AddRange(data);
        }

        return new Upgrade(datas, upgrade._abilityMarker, upgrade._isAbilityUpgrade);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="upgrade"></param>
    /// <param name="multiplier"></param>
    /// <returns>Returns new upgrade with multiplied datas</returns>
    public static Upgrade operator *(Upgrade upgrade, int multiplier)
    {
        List<UpgradeData> datas = new List<UpgradeData>();

        foreach(var data in upgrade._upgrades)
        {
            datas.Add(data * multiplier);
        }

        return new Upgrade(datas, upgrade._abilityMarker, upgrade._isAbilityUpgrade);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="upgrade"></param>
    /// <param name="multiplier"></param>
    /// <returns>Returns new upgrade with multiplied datas</returns>
    public static Upgrade operator *(Upgrade upgrade, float multiplier)
    {
        List<UpgradeData> datas = new List<UpgradeData>();

        foreach(var data in upgrade._upgrades)
        {
            datas.Add(data * multiplier);
        }

        return new Upgrade(datas, upgrade._abilityMarker, upgrade._isAbilityUpgrade);
    }
}
