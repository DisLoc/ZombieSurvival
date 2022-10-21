using System.Collections.Generic;
using UnityEngine;

public class GridXZ
{
    private int _gridSize;

    private CellMatrix _cellMatrix;
    private Vector2 _zeroCellIndex; // indexes of cell in position (0; y; 0)
    private LevelBuilder _builder;

    public GridXZ(GroundGrid groundGrid, LevelBuilder builder)
    {
        _gridSize = groundGrid.GridSize;
        _builder = builder;

        if (groundGrid.Grid.Count != _gridSize * _gridSize)
        {
            Debug.Log("Grid error");
        }
        else
        {
            _cellMatrix = new CellMatrix(_gridSize);
            int index = 0;

            for (int i = 0; i < _gridSize; i++)
            {
                for (int j = 0; j < _gridSize; j++)
                {
                    _cellMatrix[i].Add(groundGrid.Grid[index]); // fill matrix from grid prefabs
                    index++;
                }
            }

            int x = Random.Range(0, _gridSize); 
            int z = Random.Range(0, _gridSize);

            _zeroCellIndex = new Vector2(x, z); // Set random cell as start cell
        }
    }

    /// <summary>
    /// Get cell prefab in (X, Z) grid coordinates
    /// </summary>
    /// <param name="xIndex">X matrix index</param>
    /// <param name="zIndex">Z matrix index</param>
    /// <returns>Return cell prefab based on ZeroCell</returns>
    public Cell GetCell(int xIndex, int zIndex)
    {
        return _cellMatrix[Mathf.Abs(((int)_zeroCellIndex.x - xIndex) % _gridSize)][Mathf.Abs(((int)_zeroCellIndex.y) - zIndex) % _gridSize];
    }
    
    /// <summary>
    /// Use when player moves into other cell
    /// </summary>
    /// <param name="cell">Sender cell that start collide with player</param>
    public void OnPlayerEnter(Cell cell)
    {
        _builder.UpdateGrid(cell);
    }

    private class CellMatrix // matrix of cells
    {
        private List<List<Cell>> _matrix;

        public CellMatrix(int matrixSize)
        {
            _matrix = new List<List<Cell>>(matrixSize);
            for (int i = 0; i < matrixSize; i++)
            {
                _matrix.Add(new List<Cell>(matrixSize));
            }
        }

        public List<Cell> this[int index] => _matrix[index];

        public Cell this[int index1, int index2] => _matrix[index1][index2];
    }
}
