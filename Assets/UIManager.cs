using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverScreen;    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        gameOverScreen.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
