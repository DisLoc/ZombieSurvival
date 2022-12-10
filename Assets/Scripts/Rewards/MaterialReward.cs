using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MaterialReward : Reward
{
    [SerializeField] private EquipmentList _equipmentList;

    [SerializeField] private bool _hasRandomMaterialReward;
    [Tooltip("If HasRandomMaterialReward is true, field can be null")]
    [SerializeField] private EquipmentMaterial _specificMaterialReward;

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
        if (_hasRandomMaterialReward || _specificMaterialReward == null)
        {
            for (int i = 0; i < _rewardAmount; i++)
            {
                inventory.Add(_equipmentList.GetRandomMaterial());
            }
        }
        else
        {
            inventory.Add(_specificMaterialReward, _rewardAmount);
        }
    }
}
