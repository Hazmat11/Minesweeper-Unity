using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    GameObject gamemanager;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void DestroyOldGM()
    {
        gamemanager = GameObject.Find("GameManager");
        Destroy(gamemanager);
    }
}