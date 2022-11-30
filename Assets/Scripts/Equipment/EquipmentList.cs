using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Equipment/Equipment list", fileName = "New equipment list")]
public class EquipmentList : ScriptableObject
{
    [SerializeField] private List<Equipment> _allEquipment;

    public Equipment this[EquipmentData data]
    {
        get
        {
            return _allEquipment.Find(item => item.EquipmentData.Equals(data));
        }
    }

    public Equipment GetRandomEquipment(EquipRarity rarity)
    {
        List<Equipment> random = _allEquipment.FindAll(item => item.EquipRarity.Equals(rarity));

        if (random.Count > 0) return random[Random.Range(0, random.Count)];
        else return null;
    }
    
    public Equipment GetRandomEquipment(EquipSlot slot)
    {
        List<Equipment> random = _allEquipment.FindAll(item => item.EquipSlot.Equals(slot));

        if (random.Count > 0) return random[Random.Range(0, random.Count)];
        else return null;
    }

    public Equipment GetRandomEquipment(EquipSlot slot, EquipRarity rarity)
    {
        List<Equipment> random = _allEquipment.FindAll(item => item.EquipSlot.Equals(slot) && item.EquipRarity.Equals(rarity));

        if (random.Count > 0) return random[Random.Range(0, random.Count)];
        else return null;
    }
}
