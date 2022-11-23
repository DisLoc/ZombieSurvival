using UnityEngine;
using UnityEngine.UI;

public class LevelProgressBreakpoint : MonoBehaviour
{
    [SerializeField] private RectTransform _transform;

    [SerializeField] private Image _iconImage;
    [SerializeField] private Image _lineImage;

    [SerializeField] private Sprite _defaultLineSprite;
    [SerializeField] private Color _defaultLineColor;

    public RectTransform Transform => _transform;

    public void SetBreakpoint(Breakpoint breakpoint)
    {
        if (breakpoint.Icon != null)
        {
            _iconImage.sprite = breakpoint.Icon;

            if (breakpoint.Line != null)
            {
                _lineImage.sprite = breakpoint.Line;
            }
            else
            {
                _lineImage.sprite = _defaultLineSprite;
            }
            
            if (breakpoint.LineColor.a != 0)
            {
                _lineImage.color = breakpoint.LineColor;
            }
            else
            {
                _lineImage.color = _defaultLineColor;
            }
        }
        else return;
    }
}
