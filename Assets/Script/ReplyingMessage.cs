using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Security.Cryptography;

public class ReplyingMessage : MonoBehaviour
{
    public TMP_InputField inputField;  // Reference to the input field
    public GameObject chatContent;  // Parent object to hold chat bubbles
    public GameObject chatBubblePrefab;  // Prefab of the chat bubble
    public Button sendButton; // Reference to the send button
    public GameObject sendButtonSprite;
    public GameObject parentChatBubble;
    public GameObject defaultButtonSprite;
    public GameObject HintIndicatorSendMessage;
    public GameObject HintIndicatorInputText;
    public GameObject levelCompleteScreen;
    public Animator levelCompleteAnimator;
    public Button levelCompleteEnable;
    public GameObject fireflyStep2;
    public GameObject fireflyStep3;
    private TouchScreenKeyboard keyboard;
    //public LevelInstructionManager levelInstructionManager;
    private bool isCalled = false;


    void Start()
    {
        // Add a listener to detect when the input field value changes
        inputField.onValueChanged.AddListener(UpdateButtonState);
        inputField.characterLimit = 20;
        sendButton.onClick.AddListener(OnSendButtonClick);

        // Initialize button state
        UpdateButtonState(inputField.text);
        chatBubblePrefab.SetActive(false);

        isCalled = false;
    }

    void OnSendButtonClick()
    {
        SubmitText(inputField.text);
        StartCoroutine(WinScreen());
        HintIndicatorInputText.SetActive(false);
        fireflyStep2.SetActive(false);
        fireflyStep3.SetActive(false);
    }

    IEnumerator WinScreen()
    {
        yield return new WaitForSeconds(1f);
        levelCompleteScreen.SetActive(true);
        levelCompleteAnimator.SetTrigger("LevelCompleted");
    }
    void SubmitText(string text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            // Instantiate a new chat bubble from the prefab
            GameObject newBubble = Instantiate(chatBubblePrefab, parentChatBubble.transform);
            chatBubblePrefab.SetActive(true);
            // Find the TextMeshProUGUI component in the new chat bubble and set the text
            TMP_Text bubbleText = newBubble.GetComponentInChildren<TMP_Text>();
            bubbleText.text = text;

            // Adjust the size of the chat bubble
            AdjustBubbleSize(newBubble, bubbleText);

            // Activate the chat bubble prefab
            newBubble.SetActive(true);

            // Clear the input field
            inputField.text = string.Empty;

            // Update button state after sending
            UpdateButtonState(inputField.text);
        }
    }

    void AdjustBubbleSize(GameObject bubble, TMP_Text bubbleText)
    {
        // Force the text to update its mesh
        bubbleText.ForceMeshUpdate();

        // Get the preferred size of the text
        Vector2 textSize = bubbleText.GetPreferredValues(bubbleText.text);

        // Apply padding or adjustments as necessary
        float padding = 20f; // Adjust this value as needed

        // Calculate the final size of the bubble considering text size and padding
        Vector2 bubbleSize = new Vector2(textSize.x + padding, textSize.y + padding);

        // Update the size of the bubble without affecting position
        RectTransform bubbleRect = bubble.GetComponent<RectTransform>();
        bubbleRect.sizeDelta = bubbleSize;

        //Ensure the position remains unchanged by resetting anchoredPosition
        bubbleRect.anchoredPosition = Vector2.zero;
    }

    void UpdateButtonState(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            sendButtonSprite.SetActive(false);
            HintIndicatorSendMessage.SetActive(false);
            HintIndicatorInputText.SetActive(true);
            defaultButtonSprite.SetActive(true);
            fireflyStep2.SetActive(true);
            fireflyStep3.SetActive(false);
        }
        else
        {
            sendButtonSprite.SetActive(true);
            HintIndicatorInputText.SetActive(false);
            HintIndicatorSendMessage.SetActive(true);
            defaultButtonSprite.SetActive(false);
            fireflyStep2.SetActive(false);
            fireflyStep3.SetActive(true);
            
            if (!isCalled) {
                isCalled = true;
                //levelInstructionManager.NextInstruction();
            }
            
        }
    }
}