using UnityEngine;

public class DifficultyLevel : MonoBehaviour
{
    public int width;
    public int height;
    public int nbMine;
    private GameObject level;

    private void Start()
    {
        level = GameObject.Find("GameManager");
    }

    public void Easy()
    {    
        level.GetComponent<DifficultyLevel>().width = 9;
        level.GetComponent<DifficultyLevel>().height = 9;
        level.GetComponent<DifficultyLevel>().nbMine = 10;
    }
    public void Medium()
    {
        level.GetComponent<DifficultyLevel>().width = 16;
        level.GetComponent<DifficultyLevel>().height = 16;
        level.GetComponent<DifficultyLevel>().nbMine = 40;
    }
    public void Hard()
    {
        level.GetComponent<DifficultyLevel>().width = 22;
        level.GetComponent<DifficultyLevel>().height = 22;
        level.GetComponent<DifficultyLevel>().nbMine = 99;
    }
}
