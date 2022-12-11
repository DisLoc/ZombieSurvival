using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TripRewards
{
    [SerializeField] private List<TripForSuppliesReward> _levelRewards;

    private DateTime _lastGoldTime;
    private DateTime _lastExpTime;
    private DateTime _lastMaterialTime;
    private DateTime _lastEquipmentTime;

    public TripForSuppliesReward GetLevelRewards(int level)
    {
        var reward = _levelRewards.Find(item => item.RequiredLevel == level);

        if (reward == null)
        {
            int maxLevel = _levelRewards[0].RequiredLevel;

            foreach (var reward1 in _levelRewards)
            {
                if (reward1.RequiredLevel > maxLevel && reward1.RequiredLevel <= level)
                {
                    maxLevel = reward1.RequiredLevel;
                }
            }

            return _levelRewards.Find(item => item.RequiredLevel == maxLevel);
        }
        else return reward;
    }
}
