using UnityEngine;

public abstract class UIMenu : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] protected  bool _isDebug;

    [Header("Settings")]
    [SerializeField] protected CanvasGroup _canvasGroup;
    [SerializeField] protected MenuButton _button;

    protected MainMenu _mainMenu;

    public virtual void Initialize(MainMenu mainMenu)
    {
        _mainMenu = mainMenu;
    }

    public virtual void Display()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;

        if (_button != null)
        {
            _button.Enable();
        }

        if (_isDebug) Debug.Log("Display: " + name);
    }

    public virtual void Hide()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;

        if (_button != null)
        {
            _button.Disable();
        }

        if (_isDebug) Debug.Log("Hide: " + name);
    }
}
