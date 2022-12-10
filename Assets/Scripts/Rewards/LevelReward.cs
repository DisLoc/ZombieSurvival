using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelReward : Reward
{
    [SerializeField] private ExpirienceReward _expirience;
    [SerializeField] private List<CurrencyReward> _currencyRewards;
    [SerializeField] private MaterialReward _materialReward;
    [SerializeField] private EquipmentReward _equipmentReward;

    public override List<Reward> Rewards
    {
        get
        {
            List<Reward> rewards = new List<Reward>();

            rewards.Add(_expirience);
            rewards.Add(_materialReward);
            rewards.Add(_equipmentReward);
            rewards.AddRange(_currencyRewards);

            return rewards;
        }
    }

    public override void GetReward(MainInventory inventory)
    {
        _expirience.GetReward(inventory);
        _materialReward.GetReward(inventory);
        _equipmentReward.GetReward(inventory);

        foreach (CurrencyReward reward in _currencyRewards)
        {
            reward.GetReward(inventory);
        }
    }
}
