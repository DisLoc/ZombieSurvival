using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardSlot : MonoBehaviour
{
    [SerializeField] private Image _rewardIcon;
    [SerializeField] private Text _rewardAmountText;

    private List<Reward> _rewards = new List<Reward>();

    public Reward BaseReward { get; private set; }

    public void Initialize(Reward reward)
    {
        BaseReward = reward;

        _rewards.Add(reward);

        int total = 0;
        foreach (var rew in _rewards)
        {
            total += rew.RewardAmount;
        }

        _rewardIcon.sprite = reward.RewardIcon;
        _rewardAmountText.text = total.ToString();
    }
}