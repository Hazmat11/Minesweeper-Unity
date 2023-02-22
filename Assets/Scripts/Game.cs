using JetBrains.Annotations;
using UnityEngine;

public class Game : MonoBehaviour
{
    public int width = 16;
    public int height = 16;
    public int nbMine = 40;

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
        DrawMine();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Reaveal();
        }
    }

    private void Reaveal()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int Cellpos = gameBoard.tilemap.WorldToCell(worldPosition);
        Cell cell = GetCell(Cellpos.x, Cellpos.y);

        cell.isRevealed = true;
        cells[Cellpos.x, Cellpos.y] = cell;
        gameBoard.DrawBoard(cells);
    }

    private Cell GetCell(int x, int y)
    {
        if (x >= 0 && x <= 16 && y >= 0 && y <= 16)
        {
            return cells[x, y];
        }
        else
        {
            return new Cell();
        }
    }

    public void DrawMine()
    {
        for (int i = 0; i < nbMine; i++)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);
            Cell cell = new Cell();
            cell.position = new Vector3Int(x, y, 0);
            if (cells[x, y].cellType != Cell.CellType.Mine)
            {
                cell.cellType = Cell.CellType.Mine;
                cells[x, y] = cell;
            }
            else
            {
                i--;
            }

        }
    }

}
