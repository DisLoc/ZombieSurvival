using UnityEngine;

public class WeaponEquipment : Equipment
{
    [SerializeField] private Weapon _playerBaseWeaponOnEquip;

    public Weapon BaseWeapon => _playerBaseWeaponOnEquip;
}
