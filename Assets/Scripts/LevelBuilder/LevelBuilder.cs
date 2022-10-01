using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelBuilder : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    public GroundGrid groundGrid;

    [Header("Generation settings")]
    [Tooltip("Level height in y axis")]
    [SerializeField] private float _gridHeight;
    [SerializeField] private int _visionRange;

    public float GridHeight => _gridHeight;

    private GridXZ _grid;
    private GroundGrid _groundGrid;
    private int _maxDeltaIndex;

    private Dictionary<Vector2, Cell> _cells;
    private int _maxX, _minX, _maxZ, _minZ;

    [ContextMenu("Init")]
    public void Init()
    {
        _grid = new GridXZ(groundGrid, this);
        _cells = new Dictionary<Vector2, Cell>();
        _groundGrid = groundGrid;

        _maxDeltaIndex = _visionRange / _groundGrid.CellSize;
        _maxX = _maxZ = _minX = _minZ = 0;

        Vector3 pos = new Vector3(0, _gridHeight, 0);

        Cell cell = Instantiate(_grid.GetCell(0, 0), pos, Quaternion.identity, transform);
        cell.Initialize(0, 0, _grid);

        _cells.Add(new Vector2(0, 0), cell);

        UpdateGrid(cell);
    }

    [Inject]
    public void Construct(GroundGrid grid)
    {
        _grid = new GridXZ(groundGrid, this);
        _cells = new Dictionary<Vector2, Cell>();
        _groundGrid = groundGrid;

        _maxDeltaIndex = _visionRange / _groundGrid.CellSize;
        _maxX = _maxZ = _minX = _minZ = 0;

        Vector3 pos = new Vector3(0, _gridHeight, 0);

        Cell cell = Instantiate(_grid.GetCell(0, 0), pos, Quaternion.identity, transform);
        cell.Initialize(0, 0, _grid);

        _cells.Add(new Vector2(0, 0), cell);

        UpdateGrid(cell);
    }

    public void UpdateGrid(Cell enterCell)
    {
        if (_isDebug) Debug.Log("Update grid");

        int maxX = enterCell.X + _maxDeltaIndex, minX = enterCell.X - _maxDeltaIndex;
        int maxZ = enterCell.Z + _maxDeltaIndex, minZ = enterCell.Z - _maxDeltaIndex;

        if (minX < _minX) _minX = minX;
        if (maxX > _maxX) _maxX = maxX;
        if (minZ < _minZ) _minZ = minZ;
        if (maxZ > _maxZ) _maxZ = maxZ;

        for (int i = _minX; i <= _maxX; i++)
        {
            for (int j = _minZ; j <= _maxZ; j++)
            {
                Vector2 index = new Vector2(i, j);

                Vector3 pos = new Vector3
                        (
                            groundGrid.CellSize * i * 2,
                            _gridHeight,
                            groundGrid.CellSize * j * 2
                        );

                if (index.x < minX || index.x > maxX || index.y < minZ || index.y > maxZ)
                {
                    if (_cells.ContainsKey(index))
                    {
                        _cells[index].gameObject.SetActive(false);
                    }
                    else
                    {
                        Cell cell = Instantiate(_grid.GetCell(i, j), pos, Quaternion.identity, transform);
                        cell.Initialize(i, j, _grid);
                        cell.gameObject.SetActive(false);

                        _cells.Add(index, cell);
                    }
                }
                else
                {
                    if (_cells.ContainsKey(index))
                    {
                        _cells[index].gameObject.SetActive(true);
                    }
                    else
                    {
                        Cell cell = Instantiate(_grid.GetCell(i, j), pos, Quaternion.identity, transform);
                        cell.Initialize(i, j, _grid);

                        _cells.Add(index, cell);
                    }
                }
            }
        }
    }
}
