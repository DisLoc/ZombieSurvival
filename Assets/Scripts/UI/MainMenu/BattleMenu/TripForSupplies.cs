using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TripForSupplies : MonoBehaviour
{
    [Tooltip("Limit in seconds")]
    [SerializeField] private int _tripTimeLimit;

    [SerializeField] private TripRewards _tripRewards;

    [Inject] private MainInventory _mainInventory;
    [Inject] private MainMenu _mainMenu;

    private void OnEnable()
    {
        if (DataPath.Load(DataPath.Supplies) == null) // first launch
        {
            TripData data = new TripData();

            data.lastRewardTime = DateTime.Now;
            data.lastMaterialRewardTime = DateTime.Now;
            data.lastEquipmentRewardTime = DateTime.Now;

            DataPath.Save(DataPath.Supplies, data);
        }
    }

    public void OnTripClick()
    {
        var reward = _tripRewards.GetLevelRewards((int)_mainInventory.PlayerLevel.Value);

        List<Reward> rewards = new List<Reward>();
        
        if (DataPath.Load(DataPath.Supplies) is TripData data)
        {
            Debug.Log("Here");
            TripData newData = new TripData();

            DateTime lastGainedTime = data.lastRewardTime;
            DateTime currentTime = data.lastRewardTime.AddSeconds(reward.TickTime);
            DateTime maxTime = data.lastRewardTime.AddSeconds(_tripTimeLimit);
            DateTime now = DateTime.Now;

            Debug.Log(currentTime);
            Debug.Log(maxTime);

            while (currentTime <= maxTime && currentTime <= now)
            {
                rewards.Add(reward.GoldPerTick);
                rewards.Add(reward.ExpPerTick);

                lastGainedTime = currentTime;

                currentTime = currentTime.AddSeconds(reward.TickTime);
            }

            newData.lastRewardTime = lastGainedTime;

            if (reward.MaterialReward.RewardAmount > 0)
            {
                lastGainedTime = data.lastMaterialRewardTime;
                currentTime = data.lastMaterialRewardTime.AddSeconds(reward.RequiredTimeForMaterial);
                maxTime = data.lastMaterialRewardTime.AddSeconds(_tripTimeLimit);

                while (currentTime <= maxTime && currentTime <= now)
                {
                    rewards.Add(reward.MaterialReward);

                    lastGainedTime = currentTime;

                    currentTime = currentTime.AddSeconds(reward.RequiredTimeForMaterial);
                }

                newData.lastMaterialRewardTime = lastGainedTime;
            }
            else
            {
                newData.lastMaterialRewardTime = DateTime.Now;
            }

            if (reward.EquipmentReward.RewardAmount > 0)
            {
                lastGainedTime = data.lastEquipmentRewardTime;
                currentTime = data.lastEquipmentRewardTime.AddSeconds(reward.RequiredTimeForEquipment);
                maxTime = data.lastEquipmentRewardTime.AddSeconds(_tripTimeLimit);

                while (currentTime <= maxTime && currentTime <= now)
                {
                    rewards.Add(reward.EquipmentReward);

                    lastGainedTime = currentTime;

                    currentTime = currentTime.AddSeconds(reward.RequiredTimeForEquipment);
                }

                newData.lastEquipmentRewardTime = lastGainedTime;
            }
            else
            {
                newData.lastEquipmentRewardTime = DateTime.Now;
            }

            DataPath.Save(DataPath.Supplies, newData);
        }

        if (rewards.Count > 0)
        {
            _mainMenu.ShowRewards(rewards);
        }
        else
        {
            _mainMenu.ShowPopupMessage("AFK rewards is empty, try get it later");
        }
    }

    [Serializable]
    public class TripData : SerializableData
    {
        public DateTime lastRewardTime;
        public DateTime lastMaterialRewardTime;
        public DateTime lastEquipmentRewardTime;
    }
}
