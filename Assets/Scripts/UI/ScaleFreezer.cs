using UnityEngine;

public class ScaleFreezer : MonoBehaviour
{
    [SerializeField] private RectTransform _transformToScale;

    [SerializeField] private Vector2 _defaultResolution = new Vector2(1920, 1080);
    [SerializeField] private Vector2 _defaultSize;

    private void OnEnable()
    {
        Vector2 currentResolution = new Vector2(Screen.currentResolution.height, Screen.currentResolution.width);
        
        float width = _transformToScale.rect.width;
        float height = _transformToScale.rect.height;
        
        float deltaX = _defaultSize.x / height;
        float deltaY = _defaultSize.y / width;

        if (deltaX != deltaY)
        {
            float min = deltaX > deltaY ? deltaY : deltaX;

            _transformToScale.sizeDelta = new Vector2
                (
                    _defaultSize.x * min -_transformToScale.rect.width,
                    _defaultSize.y * min - _transformToScale.rect.height
                );
        }
    }
}
