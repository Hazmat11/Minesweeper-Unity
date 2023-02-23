using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyLevel : MonoBehaviour
{
    public int width;
    public int height;

    public void Easy()
    {
        width = 9;
        height = 9;
    }
    public void Medium()
    {
        width = 16;
        height = 16;
    }
    public void Hard()
    {
        width = 30;
        height = 16;
    }
}
