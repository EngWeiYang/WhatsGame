using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ReplyingMessage : MonoBehaviour, ISelectHandler
{
    public InputField inputField;  // Reference to the input field
    public Transform bot;
    public Button sendButton; // Reference to the send button
    public GameObject sendButtonSprite;
    public GameObject chatBubble;
    public GameObject defaultButtonSprite;
    public GameObject HintIndicatorSendMessage;
    public GameObject HintIndicatorInputText;
    public GameObject levelCompleteScreen;
    public Animator levelCompleteAnimator;
    public Button levelCompleteEnable;
    public GameObject fireflyStep2;
    public GameObject fireflyStep3;
    private RectTransform botRectTransform;
    public LevelInstructionManager levelInstructionManager;
    private bool isCalled = false;


    void Start()
    {
        // Add a listener to detect when the input field value changes
        inputField.onValueChanged.AddListener(UpdateButtonState);
        inputField.characterLimit = 20;
        sendButton.onClick.AddListener(OnSendButtonClick);
        botRectTransform = bot.GetComponent<RectTransform>();
        // Initialize button state
        UpdateButtonState(inputField.text);
     

        isCalled = false;
    }
    public void OnSelect(BaseEventData eventData)
    {
        activateKeyboard(inputField.text);
    }
    public void activateKeyboard(string text)
    {
        botRectTransform.anchoredPosition = new Vector2(0, 495);
    }

    void OnSendButtonClick()
    {
        SubmitText(inputField.text);
        StartCoroutine(WinScreen());
        HintIndicatorInputText.SetActive(false);
        fireflyStep2.SetActive(false);
        fireflyStep3.SetActive(false);
        botRectTransform.anchoredPosition = new Vector2(0, 0);
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
            chatBubble.SetActive(true);
            // Find the TextMeshProUGUI component in the new chat bubble and set the text
            TMP_Text bubbleText = chatBubble.GetComponentInChildren<TMP_Text>();
            bubbleText.text = text;
            // Clear the input field
            inputField.text = string.Empty;

            // Update button state after sending
            UpdateButtonState(inputField.text);
        }
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
                levelInstructionManager.NextInstruction();
            }
            
        }
    }
}