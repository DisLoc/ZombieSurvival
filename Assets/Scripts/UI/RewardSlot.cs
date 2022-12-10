using UnityEngine;
using UnityEngine.UI;

public class RewardSlot : MonoBehaviour
{
    [SerializeField] private Image _rewardIcon;
    [SerializeField] private Text _rewardAmountText;

    public void Initialize(Reward reward)
    {
        _rewardIcon.sprite = reward.RewardIcon;
        _rewardAmountText.text = reward.RewardAmount.ToString();
    }
}