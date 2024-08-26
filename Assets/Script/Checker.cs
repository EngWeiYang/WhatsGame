using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checker : MonoBehaviour
{
    public static bool isEnglish = true;
    public static bool firstTimeInScene = true;
    public GameObject levelSelectCanvas;
    public GameObject mainMenuCanvas;
    public GameObject introCanvas;

    public GameObject englishIndicator;
    public GameObject chineseIndicator;

    private void Start()
    {
        if (firstTimeInScene)
        {
            levelSelectCanvas.SetActive(false);
            introCanvas.SetActive(true);
        }
        else
        {
            levelSelectCanvas.SetActive(true);
            mainMenuCanvas.SetActive(false);
            introCanvas.SetActive(false);
        }
    }

    private void Update()
    {
        if (isEnglish)
        {
            englishIndicator.SetActive(true);
            chineseIndicator.SetActive(false);
        }
        else
        {
            englishIndicator.SetActive(false);
            chineseIndicator.SetActive(true);
        }
    }

    public void SetEnglish()
    {
        isEnglish = true;
        Debug.Log("Language is set to English");
    }

    public void SetChinese()
    {
        isEnglish = false;
        Debug.Log("Language is set to Chinese");
    }
}
