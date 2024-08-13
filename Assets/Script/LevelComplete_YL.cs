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
        //levelManager.levels[LevelSelect.currLevel].SetActive(false);
        LevelSelect.currLevel++;
        //levelManager.levels[LevelSelect.currLevel].SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LevelComplete_Restart()
    {
        PlayerPrefs.SetInt("Level" + LevelSelect.currLevel + "Complete", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Restart()
    {
        //LevelInstructionManager.currentInstruction = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        //LevelInstructionManager.currentInstruction = 0;
        PlayerPrefs.SetInt("Level" + LevelSelect.currLevel + "Complete", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }
}
