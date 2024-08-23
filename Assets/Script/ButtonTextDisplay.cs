using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class ButtonText
{
    public int buttonId;
    public string En;
    public string Cn;
}

[System.Serializable]
public class ButtonTextList
{
    public ButtonText[] buttonText;
}

public class ButtonTextDisplay : MonoBehaviour
{
    public TextAsset jsonFile;

    private ButtonTextList buttonTextList;
    public TextMeshProUGUI[] uiButtonsText;

    //public Checker checker;

    void Start()
    {
        // Load the JSON file from Resources
        if (jsonFile != null)
        {
            // Parse the JSON into our ButtonTextList class
            buttonTextList = JsonUtility.FromJson<ButtonTextList>(jsonFile.text);
        }
        else
        {
            Debug.LogError("JSON file not assigned!");
        }
    }

    private void Update()
    {
        // Assign button texts
        for (int i = 0; i < buttonTextList.buttonText.Length; i++)
        {
            if (i < uiButtonsText.Length)
            {
                if (Checker.isEnglish)
                {
                    // Assign the text from JSON to the TextMeshProUGUI component
                    uiButtonsText[i].text = buttonTextList.buttonText[i].En; // Use "Cn" for Chinese
                }
                else
                {
                    uiButtonsText[i].text = buttonTextList.buttonText[i].Cn;
                }
            }
        }
    }
}
