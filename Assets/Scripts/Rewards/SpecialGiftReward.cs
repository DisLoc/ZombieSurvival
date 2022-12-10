using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpecialGiftReward: Reward
{
    [SerializeField] private List<CurrencyReward> _currencyRewards;
    [SerializeField] private ExpirienceReward _expirienceReward;

    public override List<Reward> Rewards
    {
        get
        {
            List<Reward> rewards = new List<Reward>();

            rewards.AddRange(_currencyRewards);
            rewards.Add(_expirienceReward);

            return rewards;
        }
    }

    public override void GetReward(MainInventory inventory)
    {
        foreach (CurrencyReward currency in _currencyRewards)
            currency.GetReward(inventory);

        _expirienceReward.GetReward(inventory);
    }
}
