using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete_YL : MonoBehaviour
{
    public void NextLevelButton()
    {
        PlayerPrefs.SetInt("Level" + LevelSelect.currLevel + "Complete", 1);
        PlayerPrefs.Save();
        LevelSelect.currLevel++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LevelComplete_Restart()
    {
        PlayerPrefs.SetInt("Level" + LevelSelect.currLevel + "Complete", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {

        PlayerPrefs.SetInt("Level" + LevelSelect.currLevel + "Complete", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }
}
