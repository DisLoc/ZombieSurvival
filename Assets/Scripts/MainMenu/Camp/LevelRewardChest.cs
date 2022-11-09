using UnityEngine;
using UnityEngine.UI;

public class LevelRewardChest : MonoBehaviour
{
    [SerializeField] private Text _unlockText;
    [SerializeField] private Image _unlockImage;
    [SerializeField] private Sprite _onUnlockSprite;

    [Space(5)]
    [SerializeField] private Image _chestImage;
    [SerializeField] private Sprite _onOpenSprite;
}