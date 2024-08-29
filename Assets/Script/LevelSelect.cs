using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    // Array of levels that can be selected
    public Level[] level;

    // Sprites to represent different star statuses
    public Sprite incompleteStar;
    public Sprite greenStar;
    public Sprite orangeStar;
    public Sprite redStar;

    // Static variable to track the current level
    public static int currLevel;

    // Reference to the loading screen GameObject
    public GameObject loadingScreen;

    // Method called when a level is clicked
    public void OnClickLevel(int levelNum)
    {
        // Start loading the level asynchronously
        StartCoroutine(LoadAsynchronusly(levelNum));
    }

    // Coroutine to load a level asynchronously
    IEnumerator LoadAsynchronusly(int levelNum)
    {
        // Activate the loading screen
        loadingScreen.SetActive(true);
        Debug.Log("Loading..");

        // Wait for half a second before proceeding
        yield return new WaitForSeconds(0.5f);

        // Set the flag indicating it's not the first time in the scene
        Checker.firstTimeInScene = false;

        // Set the current level to the selected level number
        currLevel = levelNum;

        // Load the "Levels" scene asynchronously
        AsyncOperation operation = SceneManager.LoadSceneAsync("Levels");

        // Wait until the scene has finished loading
        yield return null;
    }

    // Initialize level status and stars on start
    void Start()
    {
        for (int i = 0; i < level.Length; i++)
        {
            // Check if the level is marked as completed in PlayerPrefs
            if (PlayerPrefs.GetInt("Level" + i + "Complete", 0) == 1)
            {
                // Set the star sprite based on the difficulty tag
                if (level[i].star.gameObject.tag == "Easy")
                {
                    level[i].star.sprite = greenStar;
                }
                else if (level[i].star.gameObject.tag == "Medium")
                {
                    level[i].star.sprite = orangeStar;
                }
                else if (level[i].star.gameObject.tag == "Hard")
                {
                    level[i].star.sprite = redStar;
                }
            }
            else
            {
                // Set to incomplete star if the level is not completed
                level[i].star.sprite = incompleteStar;
            }
        }
    }

    // Method to reset all levels' progress
    public void OnClickResetAllLevels()
    {
        for (int i = 0; i < level.Length; i++)
        {
            // Remove each level's completion status from PlayerPrefs
            PlayerPrefs.DeleteKey("Level" + i + "Complete");

            // Reset the star sprite to incomplete
            level[i].star.sprite = incompleteStar;
        }

        // Ensure changes are saved immediately
        PlayerPrefs.Save();
    }
}
