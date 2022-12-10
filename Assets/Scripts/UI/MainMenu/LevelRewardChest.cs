﻿using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelRewardChest : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Text _unlockTimeText;
    [SerializeField] private Image _unlockImage;

    [Space(5)]
    [SerializeField] private Image _chestImage;
    [SerializeField] private Sprite _onOpenSprite;

    private BattleMenu _menu;
    private LevelReward _reward;
    private LevelBreakpoint _breakpoint;

    public LevelReward Reward => _reward;

    public void Initialize(BattleMenu menu, string unlockText, LevelBreakpoint breakpoint)
    {
        _menu = menu;
        _unlockTimeText.text = unlockText;

        _reward = breakpoint.Reward;
        _breakpoint = breakpoint;
        
        _button.interactable = breakpoint.IsReached;

        if (breakpoint.wasClaimed)
        {
            _unlockImage.gameObject.SetActive(false);
            _chestImage.sprite = _onOpenSprite;
            _button.interactable = false;
        }
        else
        {
            _unlockImage.gameObject.SetActive(breakpoint.IsReached);
        }
    }

    public void UnlockChest()
    {
        _button.interactable = true;

        _unlockImage.gameObject.SetActive(true);
    }

    public void Open()
    {
        _unlockImage.gameObject.SetActive(false);
        _chestImage.sprite = _onOpenSprite;
        _button.interactable = false;

        _breakpoint.wasClaimed = true;

        _menu.OnRewardClick(this);
    }
}