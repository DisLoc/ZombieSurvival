using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static UnityEditor.PlayerSettings;

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
    private List<Cell> _cells;
    private int _maxDeltaIndex;

    [ContextMenu("Init")]
    public void Init()
    {
        _grid = new GridXZ(groundGrid, this);
        _cells = new List<Cell>();
        _groundGrid = groundGrid;

        _maxDeltaIndex = _visionRange / _groundGrid.CellSize;
        
        for (int i = -_maxDeltaIndex; i <= _maxDeltaIndex; i++)
            for (int j = -_maxDeltaIndex; j <= _maxDeltaIndex; j++)
            {
                Vector3 pos = new Vector3
                    (
                        groundGrid.CellSize * i * 2,
                        _gridHeight,
                        groundGrid.CellSize * j * 2
                    );

                Cell cell = Instantiate(_grid.GetCell(i, j), pos, Quaternion.identity,  transform);
                cell.Initialize(i, j, _grid);

                _cells.Add(cell);
            }
    }

    [Inject]
    public void Construct(GroundGrid grid)
    {
        _grid = new GridXZ(grid, this);
        _cells = new List<Cell>();
        _groundGrid = grid;

        _maxDeltaIndex = _visionRange / _groundGrid.CellSize;

        for (int i = -_maxDeltaIndex + 1; i < _maxDeltaIndex - 1; i++)
            for (int j = -_maxDeltaIndex + 1; j < _maxDeltaIndex - 1; j++)
            {
                Vector3 pos = new Vector3
                    (
                        groundGrid.CellSize * i * 2,
                        _gridHeight,
                        groundGrid.CellSize * j * 2
                    );

                Cell cell = Instantiate(_grid.GetCell(i, j), pos, Quaternion.identity, transform);
                cell.Initialize(i, j, _grid);

                _cells.Add(cell);
            }
    }

    public void UpdateGrid(Cell enterCell)
    {
        if (_isDebug) Debug.Log("Update grid");

        int maxX = 0, minX = 0, maxZ = 0, minZ = 0;
        
        foreach (Cell cell in _cells)
        {
            if (cell.X < minX) minX = cell.X;
            if (cell.X > maxX) maxX = cell.X;
            if (cell.Z < minZ) minZ = cell.Z;
            if (cell.Z > maxZ) maxZ = cell.Z;

            if (cell.X < enterCell.X - _maxDeltaIndex || cell.X > enterCell.X + _maxDeltaIndex ||
                cell.Z < enterCell.Z - _maxDeltaIndex || cell.Z > enterCell.Z + _maxDeltaIndex)
            {
                cell.gameObject.SetActive(false);
            }
            else
            {
                cell.gameObject.SetActive(true);
            }
        }

        if (maxX < enterCell.X + _maxDeltaIndex)
        {
            for (int i = maxX + 1; i <= enterCell.X + _maxDeltaIndex; i++)
            {
                for (int j = enterCell.Z - _maxDeltaIndex; j <= enterCell.Z + _maxDeltaIndex; j++)
                {
                    Vector3 pos = new Vector3
                    (
                        groundGrid.CellSize * i * 2,
                        _gridHeight,
                        groundGrid.CellSize * j * 2
                    );

                    Cell cell = Instantiate(_grid.GetCell(i, j), pos, Quaternion.identity, transform);
                    cell.Initialize(i, j, _grid);

                    _cells.Add(cell);
                }
            }
        }
        if (minX > enterCell.X - _maxDeltaIndex)
        {
            for (int i = enterCell.X - _maxDeltaIndex; i < minX; i++)
            {
                for (int j = enterCell.Z - _maxDeltaIndex; j <= enterCell.Z + _maxDeltaIndex; j++)
                {
                    Vector3 pos = new Vector3
                    (
                        groundGrid.CellSize * i * 2,
                        _gridHeight,
                        groundGrid.CellSize * j * 2
                    );

                    Cell cell = Instantiate(_grid.GetCell(i, j), pos, Quaternion.identity, transform);
                    cell.Initialize(i, j, _grid);

                    _cells.Add(cell);
                }
            }
        }
        if (maxZ < enterCell.Z + _maxDeltaIndex)
        {
            for (int i = enterCell.X - _maxDeltaIndex; i <= enterCell.X + _maxDeltaIndex; i++)
            {
                for (int j = maxZ + 1; j <= enterCell.Z + _maxDeltaIndex; j++)
                {
                    Vector3 pos = new Vector3
                    (
                        groundGrid.CellSize * i * 2,
                        _gridHeight,
                        groundGrid.CellSize * j * 2
                    );

                    Cell cell = Instantiate(_grid.GetCell(i, j), pos, Quaternion.identity, transform);
                    cell.Initialize(i, j, _grid);

                    _cells.Add(cell);
                }
            }
        }
        if (minZ > enterCell.Z - _maxDeltaIndex)
        {
            for (int i = enterCell.X - _maxDeltaIndex; i <= enterCell.X + _maxDeltaIndex; i++)
            {
                for (int j = enterCell.Z - _maxDeltaIndex; j < minZ; j++)
                {
                    Vector3 pos = new Vector3
                    (
                        groundGrid.CellSize * i * 2,
                        _gridHeight,
                        groundGrid.CellSize * j * 2
                    );

                    Cell cell = Instantiate(_grid.GetCell(i, j), pos, Quaternion.identity, transform);
                    cell.Initialize(i, j, _grid);

                    _cells.Add(cell);
                }
            }
        }
    }

    public void Create()
    {

    }
}
