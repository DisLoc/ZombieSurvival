using System.Collections.Generic;
using UnityEngine;

public class GridXZ
{
    private int _gridSize;
    private int _cellSize;

    private CellMatrix _cellMatrix;
    private Vector2 _zeroCellIndex; // indexes of cell in position (0; 0)
    private Transform _gridParent;

    public GridXZ(GroundGrid groundGrid, Transform gridParent)
    {
        _gridSize = groundGrid.GridSize;
        _cellSize = groundGrid.CellSize;
        _gridParent = gridParent;

        if (groundGrid.Grid.Count != _gridSize * _gridSize)
        {
            Debug.Log("Grid error");
        }
        else
        {
            _cellMatrix = new CellMatrix(_gridSize);

            for (int i = 0; i < _gridSize; i++)
            {
                for (int j = 0; j < _gridSize; j++)
                {
                    _cellMatrix[i].Add(groundGrid.Grid[j]);
                }
            }

            _gridParent = new GameObject("GroundGrid").transform;

            int x = Random.Range(0, _gridSize);
            int z = Random.Range(0, _gridSize);

            _zeroCellIndex = new Vector2(x, z);
        }
    }

    public Cell GetCell(int xIndex, int zIndex)
    {
        return _cellMatrix[Mathf.Abs(((int)_zeroCellIndex.x - xIndex) % _gridSize)][Mathf.Abs(((int)_zeroCellIndex.y) - zIndex) % _gridSize];
    }
    
    public void OnPlayerEnter(Cell cell)
    {

    }

    public class CellMatrix 
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
