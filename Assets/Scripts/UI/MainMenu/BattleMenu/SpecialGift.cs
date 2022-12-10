using System;
using UnityEngine;

public class SpecialGift : MonoBehaviour
{
    [SerializeField] private MainMenu _mainMenu;
    [Tooltip("Cooldown in seconds")]
    [SerializeField] private int _giftCooldown;

    [SerializeField] private SpecialGiftReward _reward;

    public void GetGift()
    {
        bool onCooldown;
        int cooldownTime = _giftCooldown;

        if (DataPath.Load(DataPath.SpecialGift) is GiftData loadedData)
        {
            onCooldown = (DateTime.Now - loadedData.time).TotalSeconds < _giftCooldown;
            cooldownTime = (int)(_giftCooldown - (DateTime.Now - loadedData.time).TotalSeconds);
        }
        else
        {
            onCooldown = false;
        }

        if (!onCooldown)
        {
            GiftData data = new GiftData();

            data.time = DateTime.Now;

            DataPath.Save(DataPath.SpecialGift, data);

            _mainMenu.ShowRewards(_reward.Rewards);
        } 
        else
        {
            _mainMenu.ShowPopupMessage("Gift on cooldown for " + cooldownTime);
        }
    }

    [System.Serializable]
    private class GiftData : SerializableData
    {
        public DateTime time;
    }
}
