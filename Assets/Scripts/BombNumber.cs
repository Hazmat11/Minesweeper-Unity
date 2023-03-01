using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BombNumber : MonoBehaviour
{
    public TMP_Text bombnbText;

    // Start is called before the first frame update
    void Update()
    {
        BombCount();
    }



    void BombCount()
    {
        bombnbText.text = GameObject.Find("GameManager").GetComponent<DifficultyLevel>().nbMine.ToString(); ;
    }
}
