using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
    [HideInInspector]
    public int width = 16;
    [HideInInspector]
    public int height = 16;
    [HideInInspector]
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
        else if (Input.GetMouseButtonDown(1))
        {
            Flaged();
        }
    }

    private void Reaveal()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int Cellpos = gameBoard.tilemap.WorldToCell(worldPosition);
        Cell cell = GetCell(Cellpos.x, Cellpos.y);
        if (!cell.isFlagged)
        {
            cell.isRevealed = true;
            cells[Cellpos.x, Cellpos.y] = cell;
            EmptyReveal();
            gameBoard.DrawBoard(cells);
        }
    }
    private void EmptyReveal()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (cells[i,j].cellType == Cell.CellType.Empty)
                {
                    cells[i, j].isRevealed = true;
                }

            }
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

    public void DrawNumber()
    {
        int mine = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (cells[i,j].cellType == Cell.CellType.Empty)
                {
                    if (cells[i+1,j].cellType == Cell.CellType.Mine)
                    {
                        mine++;
                    }
                    if (cells[i - 1, j].cellType == Cell.CellType.Mine)
                    {
                        mine++;
                    }
                    if (cells[i, j+1].cellType == Cell.CellType.Mine)
                    {
                        mine++;
                    }
                    if (cells[i, j - 1].cellType == Cell.CellType.Mine)
                    {
                        mine++;
                    }
                    if (cells[i + 1, j + 1].cellType == Cell.CellType.Mine)
                    {
                        mine++;
                    }
                    if (cells[i + 1, j - 1].cellType == Cell.CellType.Mine)
                    {
                        mine++;
                    }
                    if (cells[i - 1, j - 1].cellType == Cell.CellType.Mine)
                    {
                        mine++;
                    }
                    if (cells[i - 1, j + 1].cellType == Cell.CellType.Mine)
                    {
                        mine++;
                    }

                }

            }
        }
    }

}
