using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmGroupDetails : MonoBehaviour
{
    public TMP_InputField userInputField;
    public TMP_Text[] dynamicTexts; // Drag your dynamic TMP_Text objects here
    public Button confirmButton; // Drag your Button object here
    public GameObject currentScreen; // Drag your current screen GameObject here
    public GameObject nextScreen;    // Drag your next screen GameObject here

    private void Start()
    {
        if (confirmButton != null)
        {
            confirmButton.onClick.AddListener(OnConfirmButtonClick);
        }
        else
        {
            Debug.LogError("Confirm button is not assigned in the Inspector!");
        }
    }

    public void UpdateDynamicTexts(string additionalText)
    {
        // Update all TMP_Text components in the dynamicTexts array
        foreach (var text in dynamicTexts)
        {
            text.text = additionalText;
        }
    }

    private void OnConfirmButtonClick()
    {
        string userInput = userInputField.text;
        UpdateDynamicTexts(userInput);

        // Switch screens or load the next scene
        if (currentScreen != null && nextScreen != null)
        {
            currentScreen.SetActive(false);
            nextScreen.SetActive(true);
        }
        else
        {
            Debug.LogError("Screen objects are not assigned in the Inspector!");
        }
    }
}
