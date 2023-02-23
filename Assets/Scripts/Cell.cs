using UnityEngine;

public struct Cell
{
    public enum CellType
    {
        Invalid,
        Empty,
        Mine,
        Number,
        Flagged
    }

    public CellType cellType;
    public Vector3Int position;
    public int number;
    public bool isRevealed;
    public bool isFlagged;
    public bool isExploded;
}
