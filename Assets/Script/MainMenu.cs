using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public LevelObject[] levelObjects;
    public GameObject levelSelect;
    public Sprite fillStar;

    public static int currLevel;

    public void Start()
    {
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.Save();
        for (int i = 0; i < levelObjects.Length; i++)
        {
            if (PlayerPrefs.GetInt("Level" + i.ToString(), 0) == 1)
            {
                levelObjects[i].star.sprite = fillStar;
            }
        }
    }
    public void StartButton()
    {
        levelSelect.SetActive(true);
    }

    public void LoadScene(int level)
    {
        currLevel = level;
        SceneManager.LoadScene("Yilong");
    }
}
