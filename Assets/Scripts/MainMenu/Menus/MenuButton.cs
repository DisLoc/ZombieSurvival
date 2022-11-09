using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _onDisabledSprite;
    [SerializeField] private Sprite _onEnableSprite;

    public void Enable()
    {
        _image.sprite = _onEnableSprite;
    }

    public void Disable()
    {
        _image.sprite = _onDisabledSprite;
    }
}
