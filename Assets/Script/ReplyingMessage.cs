using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReplyingMessage : MonoBehaviour
{
    public InputField messageInputField;
    public Button sendButton;
    public Text chatDisplay;
    public GameObject textBox;
    public GameObject winScreen;

    void Start()
    {
        sendButton.onClick.AddListener(SendMessage);
        winScreen.SetActive(false);
    }

    void SendMessage()
    {
        string message = messageInputField.text;

        if (!string.IsNullOrEmpty(message))
        {
            // Add the message to the chat display
            chatDisplay.text += message;

            textBox.SetActive(true);

            // Clear the input field
            messageInputField.text = "";
        }
        StartCoroutine(WinScreen());
    }
    IEnumerator WinScreen()
    {
        yield return new WaitForSeconds(1);
        winScreen.SetActive(true);
    }
}
