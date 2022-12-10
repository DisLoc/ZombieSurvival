using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PackageReward : Reward
{
    [SerializeField] private List<CurrencyReward> _currencyRewards;
    [SerializeField] private List<EquipmentReward> _equipmentRewards;

    public override List<Reward> Rewards
    {
        get
        {
            List<Reward> rewards = new List<Reward>();

            rewards.AddRange(_currencyRewards);
            rewards.AddRange(_equipmentRewards);

            return rewards;
        }
    }

    public override void GetReward(MainInventory inventory)
    {
        foreach (Reward reward in Rewards)
            reward.GetReward(inventory);
    }
}
