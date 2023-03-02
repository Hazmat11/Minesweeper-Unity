using System.IO;
using UnityEngine.UI;
using UnityEngine;

public class SetPlayerName : MonoBehaviour
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
        InputField inputField = this.GetComponent<InputField>();
        string name = inputField.text;
        Debug.Log(name);
        StreamWriter sw = new StreamWriter("Assets/Sprites/name.txt");
        sw.Write(name);
        sw.Close();
    }
}
