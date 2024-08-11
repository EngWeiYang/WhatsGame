using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonSendPolling : MonoBehaviour
{
    public TMP_InputField[] userInputFields; // Array of input fields
    public TMP_Text[] dynamicTexts;          // Array of text components to update
    public Button confirmPollingDetails;     // Button to confirm and submit
    private List<string> PollingOptionsAndNames; // List of polling options and names (not used in this version)
    //public GameObject disablePollingDetailsScreen;
    public GameObject pollingText;

    // Start is called before the first frame update
    void Start()
    {
        confirmPollingDetails.onClick.AddListener(OnConfirmButtonClick);
    }

    private void OnConfirmButtonClick()
    {
        //disablePollingDetailsScreen.SetActive(false);
        pollingText.SetActive(true);
        List<string> userInputs = new List<string>();

        // Collect all user inputs from input fields
        foreach (var inputField in userInputFields)
        {
            if (inputField != null)
            {
                userInputs.Add(inputField.text);
            }
        }

        // Update dynamic texts with the collected user inputs
        UpdateDynamicTexts(userInputs);
    }

    public void UpdateDynamicTexts(List<string> inputs)
    {
        // Update TMP_Text components with user inputs
        for (int i = 0; i < dynamicTexts.Length; i++)
        {
            if (i < inputs.Count)
            {
                dynamicTexts[i].text = inputs[i];
            }
            else
            {
                dynamicTexts[i].text = string.Empty; // Clear text if not enough inputs
            }
        }
    }
}
