using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombineAbility
{
    [SerializeField] private Weapon _combinedWeapon;
    [SerializeField] private Weapon _superWeapon;
    [SerializeField] private int _unlockUpgradeLevel;

    public Weapon CombinedWeapon => _combinedWeapon;
    public Weapon SuperWeapon => _superWeapon;
    public int UnlockUpgradeLevel => _unlockUpgradeLevel;
}
