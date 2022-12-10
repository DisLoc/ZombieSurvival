using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CurrencyReward : Reward
{
    [SerializeField] private CurrencyData _currencyData;

    public CurrencyData CurrencyData => _currencyData;
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
        inventory.Add(new Currency(_currencyData, _rewardAmount));
    }
}
