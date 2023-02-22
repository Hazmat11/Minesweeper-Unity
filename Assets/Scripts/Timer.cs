using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeValue = 0;
    public float Timefixed;
    public TMP_Text timerText;
    public TMP_Text bombnbText;

    private void Start()
    {
        BombCount();
    }

    //Update is called once per frame
    void Update()
    {
        if (timeValue >= 0)
        {
            Timefixed = Mathf.Round(timeValue += Time.deltaTime);
        }
        else
        {
            timeValue = 0;
        }

        DisplayTime(timeValue);
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliseconds = timeToDisplay % 1 * 1000;

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void BombCount()
    {
        Game game = new Game();
        bombnbText.text = game.nbMine.ToString();
    }
}