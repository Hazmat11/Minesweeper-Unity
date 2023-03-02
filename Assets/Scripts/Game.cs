using UnityEngine;
using System.IO;
using System.Globalization;

public class Game : MonoBehaviour
{
    public bool isGameOver = false;
    public bool isGameVictory = false;

    private int width;
    private int height;
    public int nbMine;

    private GameBoard gameBoard;
    private GameObject level;
    private Cell[,] cells;

    public GameObject gameOver;
    public GameObject gameVictory;
    public GameObject PlaceHolder;
    public GameObject retryButton;
    public GameObject explosion;
    public GameObject confetti;

    public AudioSource clickSound;
    public AudioSource flagSound;
    public AudioSource victorySound;
    public AudioSource mineSound;

    private void Awake()
    {
        gameBoard = GetComponentInChildren<GameBoard>();
        level = GameObject.Find("GameManager");
        width = level.GetComponent<DifficultyLevel>().width;
        height = level.GetComponent<DifficultyLevel>().height;
        nbMine = level.GetComponent<DifficultyLevel>().nbMine;
    }

    void Start()
    {
        NewGame();
        Camera.main.transform.position = new Vector3(width / 2f, height / 2f, -10f);

        if (width == 22)
        {
            Camera.main.orthographicSize = 11;
        }
    }

    private void NewGame()
    {
        cells = new Cell[width, height];

        Time.timeScale = 1;
        DrawCells();
        DrawMine();
        DrawNumber();

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
        if (!isGameOver)
        {
            isGameVictory = CheckVictory();
            PrintWinningGameScene(isGameVictory);
            if (Input.GetMouseButtonDown(0))
            {
                Reveal();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                Flaged();
            }
        }
    }

    private void Reveal()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int Cellpos = gameBoard.tilemap.WorldToCell(worldPosition);
        Cell cell = GetCell(Cellpos.x, Cellpos.y);
        if (cell.cellType == Cell.CellType.Invalid || cell.isFlagged) return;

        clickSound.Play();

        if (cell.isRevealed && cell.cellType == Cell.CellType.Number)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (!isInGrid(Cellpos.x + i, Cellpos.y + j)) continue;

                    if (cells[Cellpos.x + i, Cellpos.y + j].isFlagged)
                    {
                        continue;
                    }

                    if (cells[Cellpos.x + i, Cellpos.y + j].cellType == Cell.CellType.Mine)
                    {
                        cells[Cellpos.x + i, Cellpos.y + j].isRevealed = true;
                        EndingGame(cell);
                    }

                    cells[Cellpos.x + i, Cellpos.y + j].isRevealed = true;
                }
            }
        }

        if (cell.cellType == Cell.CellType.Mine)
        {
            cell.isExploded = true;

            EndingGame(cell);
        }

        if (cell.cellType == Cell.CellType.Empty)
        {
            EmptyReveal(cell);
        }

        cell.isRevealed = true;
        cells[Cellpos.x, Cellpos.y] = cell;
        gameBoard.DrawBoard(cells);
    }

    private bool isInGrid(int x, int y) => x >= 0 && y >= 0 && x < width && y < height;
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

            EmptyReveal(GetCell(cell.position.x + 1, cell.position.y + 1));
            EmptyReveal(GetCell(cell.position.x - 1, cell.position.y - 1));
            EmptyReveal(GetCell(cell.position.x + 1, cell.position.y - 1));
            EmptyReveal(GetCell(cell.position.x - 1, cell.position.y + 1));
        }
    }
    private void Flaged()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int Cellpos = gameBoard.tilemap.WorldToCell(worldPosition);

        Cell cell = GetCell(Cellpos.x, Cellpos.y);

        if (!isInGrid(Cellpos.x, Cellpos.y)) return;
        if (!cell.isRevealed)
        {
            flagSound.Play();
        }

        cell.isFlagged = !cell.isFlagged;
        if (cell.isFlagged)
        {
            GameObject.Find("GameManager").GetComponent<DifficultyLevel>().nbMine--;
        }
        else if(!cell.isFlagged)
        {
            GameObject.Find("GameManager").GetComponent<DifficultyLevel>().nbMine++;
        }
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
        for (int i = 1; i <= nbMine; i++)
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

    private void EndingGame(Cell cell)
    {
        gameOver.SetActive(true);
        isGameOver = true;
        Time.timeScale = 0;
        retryButton.SetActive(true);

        cell.isRevealed = true;
        mineSound.Play();
        cells[cell.position.x, cell.position.y] = cell;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                cell = cells[i, j];

                if (cell.cellType == Cell.CellType.Mine)
                {
                    cell.isRevealed = true;
                    cells[i, j] = cell;
                }
            }
        }
        explosion.SetActive(true);
    }

    private void PrintWinningGameScene(bool isGameVictory)
    {
        if (isGameVictory)
        {
            isGameOver = true;
            Time.timeScale = 0;
            gameVictory.SetActive(true);
            confetti.SetActive(true);
            victorySound.Play();

            retryButton.SetActive(true);

            StreamReader sr = new StreamReader("Assets/Sprites/score.txt");
            string text = sr.ReadLine();
            sr.Close();

            int floatValue = int.Parse(text, CultureInfo.InvariantCulture.NumberFormat);

            if (floatValue > GameObject.Find("Timer").GetComponent<Timer>().Timefixed)
            {
                PlaceHolder.SetActive(true);
                StreamWriter sw = new StreamWriter("Assets/Sprites/score.txt");
                sw.Write(GameObject.Find("Timer").GetComponent<Timer>().Timefixed);
                sw.Close();
            }
        }
    }

    private bool CheckVictory()
    {
        int numberCaseRevealed = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (cells[i, j].isRevealed == true)
                {
                    numberCaseRevealed++;
                }
            }

        }

        if (numberCaseRevealed == height * width - nbMine)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
