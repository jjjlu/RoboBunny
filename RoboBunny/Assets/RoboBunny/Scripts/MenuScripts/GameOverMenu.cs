using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] GameObject gameOverMenu;

    public GameOverMenu()
    {
        // Immediately pause game when game is over.
        Time.timeScale = 0.0f;
    }

    // Returns to the first level.
    public void Restart(int sceneID)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneID);
    }

    public void MainMenu(int sceneID)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneID);
    }
}