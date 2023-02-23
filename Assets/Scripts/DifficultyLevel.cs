using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyLevel : MonoBehaviour
{
    public int width;
    public int height;
    private GameObject level;

    private void Start()
    {
        level = GameObject.Find("GameManager");
    }

    public void Easy()
    {    
        level.GetComponent<DifficultyLevel>().width = 9;
        level.GetComponent<DifficultyLevel>().height = 9;
    }
    public void Medium()
    {
        level.GetComponent<DifficultyLevel>().width = 16;
        level.GetComponent<DifficultyLevel>().height = 16;
    }
    public void Hard()
    {
        level.GetComponent<DifficultyLevel>().width = 16;
        level.GetComponent<DifficultyLevel>().height = 30;
    }

    void Update()
    {
        Debug.Log(width);
        Debug.Log(height);
    }
}
