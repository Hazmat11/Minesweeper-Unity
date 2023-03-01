using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class GetPlayerName : MonoBehaviour
{

    public TMP_Text playerName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StreamReader sr = new StreamReader("Assets/Sprites/name.txt");
        playerName.text = sr.ReadToEnd();
        sr.Close();
    }
}
