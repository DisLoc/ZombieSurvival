using UnityEngine;

public class ScaleFreezer : MonoBehaviour
{
    [SerializeField] private RectTransform _transformToScale;

    [SerializeField] private Vector2 _defaultSize;

    private void OnEnable()
    {       
        float deltaX = _transformToScale.rect.width / _defaultSize.x;
        float deltaY = _transformToScale.rect.height / _defaultSize.y;

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
