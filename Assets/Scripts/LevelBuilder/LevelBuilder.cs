using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    public GroundGrid groundGrid;

    [Header("Generation settings")]
    [Tooltip("Level height in y axis")]
    [SerializeField] private float _gridHeight;

    public float GridHeight => _gridHeight;

    private GridXZ _grid;

    [ContextMenu("Init")]
    public void Init()
    {
        _grid = new GridXZ(groundGrid, transform);
        
        for (int i = -5; i <= 5; i++)
            for (int j = -5; j <= 5; j++)
            {
                Vector3 pos = new Vector3
                    (
                        groundGrid.CellSize * i * 2,
                        _gridHeight,
                        groundGrid.CellSize * j * 2
                    );

                Cell cell = Instantiate(_grid.GetCell(i, j), pos, Quaternion.identity,  transform);
                cell.Initialize(i, j, _grid);
            }
    }

    public void Construct(GroundGrid grid)
    {
        _grid = new GridXZ(grid, transform);
    }

    public void Create()
    {

    }
}
