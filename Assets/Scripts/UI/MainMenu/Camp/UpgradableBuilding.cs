using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UpgradableBuilding : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Building settings")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Image _selfImage; 
    [SerializeField] private Sprite _lockedSprite;
    [SerializeField] private Sprite _unlockedSprite;

    [SerializeField] private int _unlockLevel;

    private bool _unlocked;
    private CampUpgrade _campUpgrade;

    public int UnlockLevel => _unlockLevel;
    public bool Unlocked => _unlocked;

    public CampUpgrade CampUpgrade => _campUpgrade;

    [Inject] private MainInventory _mainInventory;
    [Inject] private MainMenu _mainMenu;

    public void Initialize(CampUpgrade campUpgrade)
    {
        _campUpgrade = campUpgrade;
    }

    public void CheckLocked()
    {
        if (_unlocked) return;

        if (_mainInventory.PlayerLevel.Value >= _unlockLevel)
        {
            _selfImage.sprite = _unlockedSprite;
            _unlocked = true;
        }
        else
        {
            _selfImage.sprite = _lockedSprite;
            _unlocked = false;
        }
    }

    public void OnClick()
    {
        _animator.SetTrigger("Click");
    }
}
