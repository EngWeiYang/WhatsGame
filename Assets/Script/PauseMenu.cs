using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuEn;
    public GameObject pauseMenuCn;

    public void Start()
    {
        if (Checker.isEnglish)
        {
            pauseMenuEn.SetActive(true);
            pauseMenuCn.SetActive(false);
        }
        else
        {
            pauseMenuEn.SetActive(false);
            pauseMenuCn.SetActive(true);
        }
    }
    // Method to restart the current scene
    public void Restart()
    {
        // Reloads the active scene by its name
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Method to return to the main menu scene
    public void ReturnToMenu()
    {
        // Loads the scene named "MainMenu"
        SceneManager.LoadScene("MainMenu");
    }
}
