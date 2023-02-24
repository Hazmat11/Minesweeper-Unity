using UnityEngine;
using UnityEngine.Tilemaps;

public class GameBoard : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }

    public Tile tileUnknown;
    public Tile tileEmpty;
    public Tile tileMine;
    public Tile tileFlagged;
    public Tile tileExploded;
    public Tile tile1;
    public Tile tile2;
    public Tile tile3;
    public Tile tile4;
    public Tile tile5;
    public Tile tile6;
    public Tile tile7;
    public Tile tile8;


    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    public void DrawBoard(Cell[,] cells)
    {
        int width = cells.GetLength(0);
        int height = cells.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = cells[x, y];
                tilemap.SetTile(cell.position, GetTileType(cell));
            }
        }
    }

    private Tile GetTileType(Cell cell)
    {
        if (cell.isRevealed)
        {
            return GetRevealedTile(cell);
        }
        else if (cell.isFlagged)
        {
            return tileFlagged;
        }
        else
        {
            return tileUnknown;
        }
    }

    private Tile GetRevealedTile(Cell cell)
    {
        switch (cell.cellType)
        {
            case Cell.CellType.Empty:
                return tileEmpty;
            case Cell.CellType.Mine:
                return cell.isExploded ? tileExploded : tileMine;
            case Cell.CellType.Number:
                return GetNumberTile(cell); ;
            default:
                return tileUnknown;
        }
    }

    private Tile GetNumberTile(Cell cell)
    {
        switch (cell.number)
        {
            case 1: return tile1;
            case 2: return tile2;
            case 3: return tile3;
            case 4: return tile4;
            case 5: return tile5;
            case 6: return tile6;
            case 7: return tile7;
            case 8: return tile8;
            default: return null;
        }
    }


}

