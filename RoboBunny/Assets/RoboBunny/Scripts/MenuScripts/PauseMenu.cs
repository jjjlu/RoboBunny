using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Credit: https://youtu.be/tfzwyNS1LUY

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Pause()
    {
        // Activate the pause menu
        pauseMenu.SetActive(true);

        // Freeze time to pause
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        // Disable the pause menu
        pauseMenu.SetActive(false);

        // Resume time to run normally
        Time.timeScale = 1.0f;
    }

    public void MainMenu(int sceneID)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneID);
    }
}