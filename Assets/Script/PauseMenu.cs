using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
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
