using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _onDisabledSprite;
    [SerializeField] private Sprite _onEnableSprite;

    [SerializeField] private Animator _animator;

    private bool _wasEnabled;

    public void Enable()
    {
        _image.sprite = _onEnableSprite;
        
        if (!_wasEnabled)
        {
            _animator.SetTrigger("Enable");
        }

        _wasEnabled = true;
    }

    public void Disable()
    {
        _image.sprite = _onDisabledSprite;

        if (_wasEnabled)
        {
            _animator.SetTrigger("Disable");
        }

        _wasEnabled = false;
    }
}
