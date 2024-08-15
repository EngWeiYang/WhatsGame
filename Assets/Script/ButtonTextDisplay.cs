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

    void Start()
    {
        // Load the JSON file from Resources
        if (jsonFile != null)
        {
            // Parse the JSON into our ButtonTextList class
            buttonTextList = JsonUtility.FromJson<ButtonTextList>(jsonFile.text);

            // Assign button texts
            for (int i = 0; i < buttonTextList.buttonText.Length; i++)
            {
                if (i < uiButtonsText.Length)
                {
                    // Assign the text from JSON to the TextMeshProUGUI component
                    uiButtonsText[i].text = buttonTextList.buttonText[i].En; // Use "Cn" for Chinese
                }
            }
        }
        else
        {
            Debug.LogError("JSON file not assigned!");
        }
    }
}
