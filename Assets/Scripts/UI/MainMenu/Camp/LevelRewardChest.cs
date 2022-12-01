using UnityEngine;
using UnityEngine.UI;

public class LevelRewardChest : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Text _unlockTimeText;
    [SerializeField] private Image _unlockImage;

    [Space(5)]
    [SerializeField] private Image _chestImage;
    [SerializeField] private Sprite _onOpenSprite;

    private bool _opened;
    private bool _unlocked;

    private BattleMenu _menu;
    private Reward _reward;

    public Reward Reward => _reward;

    public void Initialize(BattleMenu menu, string unlockText, LevelBreakpoint breakpoint)
    {
        _menu = menu;
        _unlockTimeText.text = unlockText;

        _reward = breakpoint.Reward;
        _opened = breakpoint.wasClaimed;


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
        _unlocked = true;
        _button.interactable = true;

        _unlockImage.gameObject.SetActive(true);
    }

    public void Open()
    {
        _unlockImage.gameObject.SetActive(false);
        _chestImage.sprite = _onOpenSprite;
        _button.interactable = false;

        _menu.OnRewardClick(this);
    }
}