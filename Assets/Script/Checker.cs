using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checker : MonoBehaviour
{
    // Static variables to track the language setting and first-time scene status
    public static bool isEnglish = true; // Indicates if English is selected
    public static bool firstTimeInScene = true; // Indicates if it's the first time in the scene

    // References to various UI canvases, assigned in the Inspector
    public GameObject levelSelectCanvas;
    public GameObject mainMenuCanvas;
    public GameObject introCanvas;

    // References to UI indicators for language selection, assigned in the Inspector
    public GameObject englishIndicator;
    public GameObject chineseIndicator;

    private void Start()
    {
        // Initialize UI visibility based on the first-time-in-scene status
        if (firstTimeInScene)
        {
            levelSelectCanvas.SetActive(false); // Hide level selection canvas
            introCanvas.SetActive(true); // Show introduction canvas
        }
        else
        {
            levelSelectCanvas.SetActive(true); // Show level selection canvas
            mainMenuCanvas.SetActive(false); // Hide main menu canvas
            introCanvas.SetActive(false); // Hide introduction canvas
        }
    }

    private void Update()
    {
        // Update language indicators based on the selected language
        if (isEnglish)
        {
            englishIndicator.SetActive(true); // Show English indicator
            chineseIndicator.SetActive(false); // Hide Chinese indicator
        }
        else
        {
            englishIndicator.SetActive(false); // Hide English indicator
            chineseIndicator.SetActive(true); // Show Chinese indicator
        }
    }

    // Method to set the language to English
    public void SetEnglish()
    {
        isEnglish = true; // Set language to English
        //Debug.Log("Language is set to English"); // Log the language change
    }

    // Method to set the language to Chinese
    public void SetChinese()
    {
        isEnglish = false; // Set language to Chinese
        //Debug.Log("Language is set to Chinese"); // Log the language change
    }
}
