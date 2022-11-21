using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerUpgrades
{
    [SerializeField] private List<PlayerUpgrade> _upgrades;

    public PlayerUpgrade GetUpgrade(int level) => _upgrades.Find(item => item.RequiredLevel == level);
}