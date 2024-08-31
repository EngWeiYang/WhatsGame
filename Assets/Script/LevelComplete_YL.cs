using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete_YL : MonoBehaviour
{
    public GameObject enPanel;
    public GameObject cnPanel;

    public void Start()
    {
        if (Checker.isEnglish)
        {
            enPanel.SetActive(true);
            cnPanel.SetActive(false);
        }
        else
        {
            enPanel.SetActive(false);
            cnPanel.SetActive(true);
        }
    }
    // Called when the player clicks the "Next Level" button
    public void NextLevelButton()
    {
        // Mark the current level as complete in PlayerPrefs
        PlayerPrefs.SetInt("Level" + LevelSelect.currLevel + "Complete", 1);
        PlayerPrefs.Save(); // Save PlayerPrefs to make sure the data is stored

        // Increment the current level to move to the next level
        LevelSelect.currLevel++;

        // Reload the current scene to load the next level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Called when the player clicks the "Restart Level" button
    public void LevelComplete_Restart()
    {
        // Mark the current level as complete in PlayerPrefs
        PlayerPrefs.SetInt("Level" + LevelSelect.currLevel + "Complete", 1);
        PlayerPrefs.Save(); // Save PlayerPrefs to make sure the data is stored

        // Reload the current scene to restart the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Called when the player clicks the "Main Menu" button
    public void MainMenu()
    {
        // Mark the current level as complete in PlayerPrefs
        PlayerPrefs.SetInt("Level" + LevelSelect.currLevel + "Complete", 1);
        PlayerPrefs.Save(); // Save PlayerPrefs to make sure the data is stored

        // Load the Main Menu scene
        SceneManager.LoadScene("MainMenu");
    }
}
