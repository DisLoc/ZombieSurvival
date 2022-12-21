using System.Collections.Generic;

[System.Serializable]
public class ExpirienceReward : Reward
{
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
        inventory.PlayerLevel.AddExp(_rewardAmount);
        inventory.SavePlayer();
    }
}
