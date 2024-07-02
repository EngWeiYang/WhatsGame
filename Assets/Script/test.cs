using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{
    public void CompleteLevel(int levelNumber)
    {
        PlayerPrefs.SetInt("Level" + levelNumber + "Completed", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }
}
