using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelBuilder : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Generation settings")]
    [Tooltip("Level height in y axis")]
    [SerializeField] private float _gridHeight;
    [Tooltip("Distance from player for level following")]
    [SerializeField] private int _visionRange;

    public float GridHeight => _gridHeight;

    private GridXZ _grid;
    private GroundGrid _groundGrid;
    private int _maxDeltaIndex;

    /// <summary>
    /// Existing cells on scene
    /// </summary>
    private Dictionary<Vector2, Cell> _cells;
    /// <summary>
    /// Extreme index of grid
    /// </summary>
    private int _maxX, _minX, _maxZ, _minZ;

    [Inject]
    public void Construct(GroundGrid grid)
    {
        _grid = new GridXZ(grid, this);
        _cells = new Dictionary<Vector2, Cell>();
        _groundGrid = grid;

        _maxDeltaIndex = Mathf.RoundToInt((float)_visionRange / _groundGrid.CellSize);
        _maxX = _maxZ = _minX = _minZ = 0;

        Vector3 pos = new Vector3(0, _gridHeight, 0);

        Cell cell = GetCell(0, 0, pos);
        cell.Initialize(0, 0, _grid);

        _cells.Add(new Vector2(0, 0), cell);

        UpdateGrid(cell);
    }

    /// <summary>
    /// Move level following the player
    /// </summary>
    /// <param name="enterCell">Cell that player enter</param>
    public void UpdateGrid(Cell enterCell)
    {
        if (_isDebug) Debug.Log("Update grid");

        int maxX = enterCell.X + _maxDeltaIndex, minX = enterCell.X - _maxDeltaIndex;
        int maxZ = enterCell.Z + _maxDeltaIndex, minZ = enterCell.Z - _maxDeltaIndex;

        // calculate new extreme indexes
        if (minX < _minX) _minX = minX;
        if (maxX > _maxX) _maxX = maxX;
        if (minZ < _minZ) _minZ = minZ;
        if (maxZ > _maxZ) _maxZ = maxZ;

        for (int i = _minX; i <= _maxX; i++)
        {
            for (int j = _minZ; j <= _maxZ; j++)
            {
                // current cell index
                Vector2 index = new Vector2(i, j);

                // current cell position
                Vector3 pos = new Vector3
                        (
                            _groundGrid.CellSize * i * 2,
                            _gridHeight,
                            _groundGrid.CellSize * j * 2
                        );

                if (index.x < minX || index.x > maxX || index.y < minZ || index.y > maxZ)
                {
                    // level always must be a square (including disabled cells)

                    if (_cells.ContainsKey(index))
                    {
                        _cells[index].gameObject.SetActive(false);
                    }
                    else
                    {
                        Cell cell = GetCell(i, j, pos); 

                        cell.Initialize(i, j, _grid);
                        cell.gameObject.SetActive(false);

                        _cells.Add(index, cell);
                    }
                }
                else
                {
                    // level always must be a square (including enabled cells)

                    if (_cells.ContainsKey(index))
                    {
                        _cells[index].gameObject.SetActive(true);
                    }
                    else
                    {
                        Cell cell = GetCell(i, j, pos);
                        cell.Initialize(i, j, _grid);

                        _cells.Add(index, cell);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Get cell from matrix and add to scene
    /// </summary>
    /// <param name="xIndex">X index in grid</param>
    /// <param name="zIndex">Z index in grid</param>
    /// <param name="pos">Position need to place this cell</param>
    /// <returns>Return placed cell</returns>
    private Cell GetCell(int xIndex, int zIndex, Vector3 pos)
    {
        Cell cell = _grid.GetCell(xIndex, zIndex);
        cell = Instantiate(cell, pos, cell.RotationQuaternion, transform);

        return cell;
    }
}
