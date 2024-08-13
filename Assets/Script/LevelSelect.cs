using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public Level[] level;
    public Sprite incompleteStar;
    public Sprite greenStar;
    public Sprite orangeStar;
    public Sprite redStar;
    public static int currLevel;
    public GameObject loadingScreen;

    public void OnClickLevel(int levelNum)
    {
        StartCoroutine(LoadAsynchronusly(levelNum));
    }

    IEnumerator LoadAsynchronusly(int levelNum)
    {
        //Loading screen
        loadingScreen.SetActive(true);
        Debug.Log("Loading..");

        yield return new WaitForSeconds(0.5f);

        Checker.firstTimeInScene = false;
        currLevel = levelNum;
        AsyncOperation operation = SceneManager.LoadSceneAsync("Levels");

        yield return null;
    }

    void Start()
    {
        for(int i = 0; i < level.Length; i++)
        {
            if (PlayerPrefs.GetInt("Level" + i + "Complete", 0) == 1)
            {
                if (level[i].star.gameObject.tag == "Easy")
                {
                    level[i].star.sprite = greenStar;
                }
                else if(level[i].star.gameObject.tag == "Medium")
                {
                    level[i].star.sprite = orangeStar;
                }
                else if(level[i].star.gameObject.tag == "Hard")
                {
                    level[i].star.sprite = redStar;
                }
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
