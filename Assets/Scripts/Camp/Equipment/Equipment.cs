using UnityEngine;

public class Equipment : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Equipment settings")]
    [SerializeField] private EquipSlots _equipSlot;
    [SerializeField] private EquipRarity _rarity;



    public Upgrade EquipUpgrade => null;
}
