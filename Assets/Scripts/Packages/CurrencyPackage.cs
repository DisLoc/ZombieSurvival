using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CurrencyPackage : RewardPackage
{
    [SerializeField] private List<CurrencyReward> _currencyRewards;

    [Inject] private MainMenu _mainMenu;

    public void OnClick()
    {
        List<Reward> rewards = new List<Reward>();

        foreach(var reward in _currencyRewards)
        {
            rewards.AddRange(reward.Rewards);
        }

        _mainMenu.ShowRewards(rewards);
    }
}
