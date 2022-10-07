using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Upgrades/Upgrade", fileName = "New upgrade")]
public class Upgrade : ScriptableObject
{
    [SerializeField] private List<UpgradeData> _upgrades;

    public List<UpgradeData> Upgrades => _upgrades;
}
