using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BombNumber : MonoBehaviour
{
    public TMP_Text bombnbText;

    // Start is called before the first frame update
    void Start()
    {
        BombCount();
    }

    void BombCount()
    {
        Game game = gameObject.AddComponent(typeof(Game)) as Game;
        bombnbText.text = game.nbMine.ToString();
    }
}
