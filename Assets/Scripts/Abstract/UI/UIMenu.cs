using UnityEngine;

public abstract class UIMenu : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] protected  bool _isDebug;

    [Header("Settings")]
    [SerializeField] protected CanvasGroup _canvasGroup;
    [Tooltip("Button that displays this menu (can be null)")]
    [SerializeField] protected MenuButton _button;

    [Space(5)]
    [SerializeField] protected Animator _animator;

    protected MainMenu _mainMenu;
    protected UIMenu _parentMenu;

    public virtual void Initialize(MainMenu mainMenu, UIMenu parentMenu = null)
    {
        _mainMenu = mainMenu;
        _parentMenu = parentMenu;

        _animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        _animator.cullingMode = AnimatorCullingMode.CullCompletely;
    }
    public virtual void Display(bool playAnimation = false)
    {
        if (_animator != null && playAnimation)
        {
            if (_isDebug) Debug.Log("Display by animator: " + name);

            _animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
            _animator.SetTrigger("Display");
        }
        else
        {
            _animator.cullingMode = AnimatorCullingMode.CullCompletely;

            _canvasGroup.alpha = 1;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;

            if (_isDebug) Debug.Log("Just display: " + name);
        }

        if (_button != null)
        {
            _button.Enable();
        }
    }

    public virtual void Hide(bool playAnimation = false)
    {
        if (_animator != null && playAnimation)
        {
            if (_isDebug) Debug.Log("Hide by animator: " + name);

            _animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
            _animator.SetTrigger("Hide");
        }
        else
        {
            _animator.cullingMode = AnimatorCullingMode.CullCompletely;

            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;

            if (_isDebug) Debug.Log("Just hide: " + name);
        }

        if (_button != null)
        {
            _button.Disable();
        }
    }

    public void MainMenuDisplay()
    {
        _mainMenu.Display(this);
    }

    public void MainMenuHide()
    {
        _mainMenu.DisplayDefault();
    }

    #if UNITY_EDITOR
    [ContextMenu("Display with animation")]
    protected void DisplayAnimation()
    {
        Display(true);
    }

    [ContextMenu("Hide with animation")]
    protected void HideAnimation()
    {
        Hide(true);
    }
    #endif
}
