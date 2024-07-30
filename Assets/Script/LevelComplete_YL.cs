using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete_YL : MonoBehaviour
{
   public void OnLevelComplete()
    {
        PlayerPrefs.SetInt("Level" + LevelSelect.currLevel + "Complete", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }
}
