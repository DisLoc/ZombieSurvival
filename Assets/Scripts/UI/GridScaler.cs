using UnityEngine;
using UnityEngine.UI;

public class GridScaler : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup _grid;
    [SerializeField] private int _maxItemsInRow = 6;

    [SerializeField] private Vector2 _defaultResolution = new Vector2(1080, 1920);

    [SerializeField] private Vector2 _defaultCellSize;
    [SerializeField] private Vector2 _defaultSpacing;

    public GridLayoutGroup Grid => _grid;

    public int MaxItemsInRow => _maxItemsInRow;

    private void OnEnable()
    {
        Vector2 resolution = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);

        _grid.cellSize = new Vector2(resolution.x * _defaultCellSize.x / _defaultResolution.x, resolution.y * _defaultCellSize.y / _defaultResolution.y);
        _grid.spacing = new Vector2(resolution.x * _defaultSpacing.x / _defaultResolution.x, resolution.y * _defaultSpacing.y / _defaultResolution.y);
    }
}
