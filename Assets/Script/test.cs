using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public void OnClickComplete()
    {
        int previousLevel = MainMenu.currLevel - 1;

        if (previousLevel >= 0)
        {
            PlayerPrefs.SetInt("Level" + previousLevel.ToString(), 1);
        }
        SceneManager.LoadScene("MainMenu");
    }
}
