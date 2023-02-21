using UnityEngine;

public class Game : MonoBehaviour
{
    public int width = 16;
    public int height = 16;

    private GameBoard gameBoard;
    private Cell[,] cells;

    private void Awake()
    {
        gameBoard = GetComponentInChildren<GameBoard>();
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        cells = new Cell[width, height];
        DrawCells();

        Camera.main.transform.position = new Vector3(width / 2f, height / 2f, -10f);
        gameBoard.DrawBoard(cells);
    }

    private void DrawCells()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = new Cell();
                cell.position = new Vector3Int(x, y, 0);
                cell.cellType = Cell.CellType.Empty;
                cells[x, y] = cell;
            }
        }
    }
}
