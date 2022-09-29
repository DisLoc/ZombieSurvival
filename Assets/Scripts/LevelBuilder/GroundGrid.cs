using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ZombieSurvival/LevelBuilder/GroundGrid", fileName = "New ground grid")]
public class GroundGrid : ScriptableObject
{
    [Tooltip("")]
    [SerializeField] private int _gridSize;
    [SerializeField] private int _cellSize;
    [SerializeField] List<Cell> _grid;

    public int GridSize => _gridSize;
    public int CellSize => _cellSize;
    public List<Cell> Grid => _grid;
}
