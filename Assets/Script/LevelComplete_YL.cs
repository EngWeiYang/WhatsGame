using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete_YL : MonoBehaviour
{
    public LevelManager levelManager;
    public void NextLevelButton()
    {
        PlayerPrefs.SetInt("Level" + LevelSelect.currLevel + "Complete", 1);
        PlayerPrefs.Save();
        levelManager.levels[LevelSelect.currLevel].SetActive(false);
        LevelSelect.currLevel++;
        levelManager.levels[LevelSelect.currLevel].SetActive(true);
    }
    public void LevelComplete_Restart()
    {
        PlayerPrefs.SetInt("Level" + LevelSelect.currLevel + "Complete", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        PlayerPrefs.SetInt("Level" + LevelSelect.currLevel + "Complete", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }
}
