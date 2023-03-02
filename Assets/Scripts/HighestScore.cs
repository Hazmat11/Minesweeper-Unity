using System.Globalization;
using System.IO;
using TMPro;
using UnityEngine;

public class HighestScore : MonoBehaviour
{

    public TMP_Text bestScore;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        showBestScore();
    }

    void showBestScore()
    {
        StreamReader sr = new StreamReader(Application.persistentDataPath + "score.txt");
        string text = sr.ReadToEnd();

        int floatValue = int.Parse(text, CultureInfo.InvariantCulture.NumberFormat);

        float minutes = Mathf.FloorToInt(floatValue / 60);
        float seconds = Mathf.FloorToInt(floatValue % 60);

        bestScore.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        sr.Close();
    }
}
