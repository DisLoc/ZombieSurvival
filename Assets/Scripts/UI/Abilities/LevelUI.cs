using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private Image _image;

    [Space(5)]
    [SerializeField] private Sprite _unlockedLevel;
    [SerializeField] private Sprite _lockedLevel;
    [SerializeField] private Sprite _currentLevel;

    /// <summary>
    /// Set level sprite based on type
    /// </summary>
    /// <param name="type"></param>
    public void Initialize(LevelType type)
    {
        Sprite sprite;

        switch (type)
        {
            case LevelType.Current:
                sprite = _currentLevel;
                break;
            case LevelType.Unlocked:
                sprite = _unlockedLevel;
                break;
            case LevelType.Locked:
                sprite = _lockedLevel;
                break;
            default:
                if (_isDebug) Debug.Log("Missing LevelType!");

                sprite = _lockedLevel;
                break;
        }

        _image.sprite = sprite;
    }

    public enum LevelType
    {
        Locked,
        Unlocked,
        Current
    }
}
