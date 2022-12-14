using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RewardsInfo : UIMenu
{
    [Header("RewardInfo settings")]
    [SerializeField] private RewardSlot _rewardSlotPrefab;
    [SerializeField] private Transform _rewardSlotsParent;

    private List<RewardSlot> _slots = new List<RewardSlot>();

    [Inject] private MainInventory _mainInventory;

    public void ShowReward(List<Reward> rewards)
    {
        if (_slots.Count > 0)
        {
            foreach (var slot in _slots)
            {
                Destroy(slot.gameObject);
            }

            _slots.Clear();
        }

        foreach(Reward reward in rewards)
        {
            if (reward == null && reward.RewardAmount == 0)
            {
                if (_isDebug) Debug.Log("Missing reward!");

                continue;
            }

            RewardSlot findedSlot = _slots.Find(item => item.BaseReward.RewardIcon.Equals(reward.RewardIcon));

            if (findedSlot != null)
            {
                findedSlot.Initialize(reward);
            }
            else
            {
                RewardSlot slot = Instantiate(_rewardSlotPrefab, _rewardSlotsParent);

                slot.Initialize(reward);

                _slots.Add(slot);
            }

            reward.GetReward(_mainInventory);
        }
    }
}
