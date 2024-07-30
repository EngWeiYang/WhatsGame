using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public Level[] level;
    public Sprite completeStar;
    public Sprite incompleteStar;
    public static int currLevel;

    public void OnClickLevel(int levelNum)
    {
        currLevel = levelNum;
        SceneManager.LoadScene("Levels");
    }

    void Start()
    {
        for(int i = 0; i < level.Length; i++)
        {
            if (PlayerPrefs.GetInt("Level" + i + "Complete", 0) == 1)
            {
                level[i].star.sprite = completeStar;
            }
            else
            {
                level[i].star.sprite = incompleteStar; // Set to incomplete star if not completed
            }
        }
    }

    public void OnClickResetAllLevels()
    {
        for (int i = 0; i < level.Length; i++)
        {
            // Remove each level's completion status from PlayerPrefs
            PlayerPrefs.DeleteKey("Level" + i + "Complete");
            level[i].star.sprite = incompleteStar; // Reset the star sprite
        }
        PlayerPrefs.Save(); // Ensure changes are saved immediately
    }
}
