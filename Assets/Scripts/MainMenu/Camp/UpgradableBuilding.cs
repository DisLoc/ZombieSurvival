using System.Collections.Generic;
using UnityEngine;

public class UpgradableBuilding : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Building settings")]
    [SerializeField] private Level _level;
    [SerializeField] private List<CurrentUpgrade> _buildingUpgrade;

    public Upgrade BuildingUpgrade
    {
        get
        {
            //Upgrade upgrade = new Upgrade();

            return null;
        }
    }

    public void LevelUp()
    {
        _level.LevelUp();
    }

    public void Unlock()
    {

    }
}
