using UnityEngine;

public class ScaleFreezer : MonoBehaviour
{
    [SerializeField] private RectTransform _transformToScale;

    [SerializeField] private Vector2 _defaultSize;

    private void OnEnable()
    {       
        float deltaX = _defaultSize.x / _transformToScale.rect.width;
        float deltaY = _defaultSize.y / _transformToScale.rect.height;

        if (deltaX != deltaY)
        {
            float min = deltaX > deltaY ? deltaY : deltaX;

            _transformToScale.sizeDelta = new Vector2
                (
                    (_defaultSize.x * min - _transformToScale.rect.width),
                    (_defaultSize.y * min - _transformToScale.rect.height)
                );
        }
    }
}
