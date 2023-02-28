using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Name : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewValue()
    {
        StreamWriter sw = new StreamWriter("Assets/Sprites/name.txt");
        Debug.Log(this.gameObject.name);
/*        sw.WriteLine();*/
        sw.Close();
    }   
}
