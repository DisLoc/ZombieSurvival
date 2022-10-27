using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/Level/GroundGrid", fileName = "New ground grid")]
public class GroundGrid : ScriptableObject
{
    [SerializeField] private int _cellSize;
    [Tooltip("Cells must be a square")]
    [SerializeField] List<Cell> _grid;

    /// <summary>
    /// Matrix size (N x N)
    /// </summary>
    public int GridSize => (int)Mathf.Sqrt(_grid.Count);
    /// <summary>
    /// Side lenght of cell
    /// </summary>
    public int CellSize => _cellSize;
    public List<Cell> Grid => _grid;
}
