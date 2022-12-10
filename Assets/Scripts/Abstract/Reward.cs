using System.Collections.Generic;
using UnityEngine;

public abstract class Reward
{
    [SerializeField] protected Sprite _rewardIcon;
    [SerializeField] protected int _rewardAmount;

    public Sprite RewardIcon => _rewardIcon;
    public int RewardAmount => _rewardAmount;
    
    public abstract List<Reward> Rewards { get; }

    public abstract void GetReward(MainInventory inventory);
} 
