using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipmentReward : Reward
{
    [SerializeField] private EquipmentList _equipmentList;

    [SerializeField] private bool _hasRandomEquipmentReward;
    [Tooltip("If HasRandomEquipmentReward is true, must fill that field")]
    [SerializeField] private EquipRarity _randomEquipmentRarity;
    [Tooltip("If HasRandomEquipmentReward is true, field can be null")]
    [SerializeField] private Equipment _specificEquipmentReward;

    public override List<Reward> Rewards
    {
        get
        {
            List<Reward> rewards = new List<Reward>();

            rewards.Add(this);

            return rewards;
        }
    }

    public override void GetReward(MainInventory inventory)
    {
        if (_hasRandomEquipmentReward || _specificEquipmentReward == null)
        {
            for (int i = 0; i < _rewardAmount; i++)
            {
                inventory.Add(_equipmentList.GetRandomEquipment(_randomEquipmentRarity));
            }
        }
        else
        {
            inventory.Add(_specificEquipmentReward, _rewardAmount);
        }
    }
}
