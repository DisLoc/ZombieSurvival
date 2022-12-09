using UnityEngine;

public class ScaleFreezer : MonoBehaviour
{
    [SerializeField] private RectTransform _transformToScale;

    [SerializeField] private Vector2 _defaultResolution = new Vector2(1920, 1080);
    [SerializeField] private Vector2 _defaultSize;

    private void OnEnable()
    {
        Vector2 currentResolution = new Vector2(Screen.currentResolution.height, Screen.currentResolution.width);

        float deltaX = _transformToScale.rect.height / _defaultSize.x;
        float deltaY = _transformToScale.rect.width / _defaultSize.y;

        if (deltaX != deltaY)
        {
            float min = deltaX > deltaY ? deltaY : deltaX;

            _transformToScale.rect.Set(0, 0, _defaultSize.y * min, _defaultSize.x * min);
        }
    }
}
