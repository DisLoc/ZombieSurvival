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

    public void Initialize(BattleMenu menu, string unlockText, bool unlocked = false, bool claimed = false)
    {
        _menu = menu;
        _unlockTimeText.text = unlockText;

        _opened = claimed;

        if (unlocked && !claimed)
        {
            UnlockChest();
        }
        else
        {
            _unlocked = unlocked;
        }

        if (claimed)
        {
            _unlockImage.gameObject.SetActive(false);
            _chestImage.sprite = _onOpenSprite; 
            _button.interactable = false;
        }

        if (!unlocked) _button.interactable = false;
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