using UnityEngine;
using UnityEngine.UI;

public class GridScaler : MonoBehaviour
{
    [SerializeField] private RectTransform _gridTransform;
    [SerializeField] private bool _changeHeight;

    [Space(5)]
    [SerializeField] private Vector2 _delaultResolution;
    [SerializeField] private GridLayoutGroup _grid;
    [SerializeField] private Vector2 _defaultCellSize;
    [SerializeField] private Vector2 _defaultSpacing;
    [SerializeField] private Vector4 _defaultPadding;

    private float _min;

    private void OnEnable()
    {
        float deltaX = Screen.width / _delaultResolution.x;
        float deltaY = Screen.height / _delaultResolution.y;

        float min = deltaX > deltaY ? deltaY : deltaX;

        _min = min;

        _grid.cellSize = new Vector2(_defaultCellSize.x * min, _defaultCellSize.y * min);
        _grid.spacing = new Vector2(_defaultSpacing.x * min, _defaultSpacing.y * min);

        _grid.padding.left = (int)(_defaultPadding.x * min);
        _grid.padding.right = (int)(_defaultPadding.y * min);
        _grid.padding.top = (int)(_defaultPadding.w * min);
        _grid.padding.bottom = (int)(_defaultPadding.z * min);

        if (_changeHeight)
            _gridTransform.sizeDelta = new Vector2(0, _gridTransform.rect.height * min);
    }
}