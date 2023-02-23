using JetBrains.Annotations;
using Unity.VisualScripting;
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
        DrawMine();
        DrawNumber();

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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Reveal();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Flaged();
        }
    }

    private void Reveal()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int Cellpos = gameBoard.tilemap.WorldToCell(worldPosition);
        Cell cell = GetCell(Cellpos.x, Cellpos.y);
        if (cell.cellType == Cell.CellType.Invalid || cell.isFlagged || cell.isRevealed) return;

        if (cell.cellType == Cell.CellType.Empty)
        {
            EmptyReveal(cell);
        }

        cell.isRevealed = true;
        cells[Cellpos.x, Cellpos.y] = cell;
        gameBoard.DrawBoard(cells);

    }
    private void EmptyReveal(Cell cell)
    {
        if (cell.cellType == Cell.CellType.Invalid || cell.isRevealed) return;
        if (cell.cellType == Cell.CellType.Mine) return;

        cell.isRevealed = true;
        cells[cell.position.x, cell.position.y] = cell;

        if (cell.cellType == Cell.CellType.Empty)
        {
            EmptyReveal(GetCell(cell.position.x - 1, cell.position.y));
            EmptyReveal(GetCell(cell.position.x + 1, cell.position.y));
            EmptyReveal(GetCell(cell.position.x, cell.position.y - 1));
            EmptyReveal(GetCell(cell.position.x, cell.position.y + 1));
        }
    }
    private void Flaged()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int Cellpos = gameBoard.tilemap.WorldToCell(worldPosition);

        Cell cell = GetCell(Cellpos.x, Cellpos.y);

        cell.isFlagged = !cell.isFlagged;
        cells[Cellpos.x, Cellpos.y] = cell;
        gameBoard.DrawBoard(cells);
    }

    private Cell GetCell(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return cells[x, y];
        }
        else
            return new Cell();
    }

    public void DrawMine()
    {
        for (int i = 0; i <= nbMine; i++)
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

    private void DrawNumber()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Cell cell = cells[i, j];

                if (cell.cellType == Cell.CellType.Mine)
                {
                    continue;
                }

                cell.number = CountMine(i, j);

                if (cell.number > 0)
                {
                    cell.cellType = Cell.CellType.Number;
                }

                cells[i, j] = cell;
            }
        }
    }

    public int CountMine(int cellXpos, int cellYpos)
    {
        int mine = 0;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }

                int x = cellXpos + i;
                int y = cellYpos + j;

                if (x < 0 || x >= width || y < 0 || y >= height)
                {
                    continue;
                }

                if (GetCell(x, y).cellType == Cell.CellType.Mine)
                {
                    mine++;
                }
            }
        }
        return mine;
    }
}
