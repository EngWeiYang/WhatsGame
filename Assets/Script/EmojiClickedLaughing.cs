using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmojiClickedLaughing : MonoBehaviour
{
    public Button laughingEmojiBtn;
    public TMP_Text emojiLaughing;
    public TMP_Text emojiLaughinginMessage;
    public TMP_Text text;
    public GameObject sendMessageBtn;
    public GameObject textMessageWithEmojiLaughing;
    public Button sendMessageText;
    public Animator animator;
    public Animator animatorSendingMessage;
    public GameObject defaultMessageBtn;
    public Button toNextScenario;
    public GameObject Scenario2;
    public GameObject Scenario1;
    public GameObject hintIndicatorEmoji;
    public GameObject hintIndicatorEmojiClick;
    public GameObject hintIndicatorSendEmoji;
    public GameObject firefly1;
    public GameObject firefly2;


    void Start()
    {
        laughingEmojiBtn.onClick.AddListener(clickedLaughingEmoji);
        sendMessageText.onClick.AddListener(sendMessage);
        toNextScenario.onClick.AddListener(proceedToNextScenario);
        Color laughingColor = emojiLaughing.color;
        laughingColor.a = 0f;
        emojiLaughing.color = laughingColor;
    }

    void clickedLaughingEmoji()
    {
        Color laughingColor = emojiLaughing.color;
        laughingColor.a = 1f;
        emojiLaughing.color = laughingColor;
        text.gameObject.SetActive(false);
        sendMessageBtn.gameObject.SetActive(true);
        hintIndicatorEmojiClick.gameObject.SetActive(false);
        firefly1.gameObject.SetActive(false);
        firefly2.gameObject.SetActive(true);
        defaultMessageBtn.gameObject.SetActive(false);
        hintIndicatorEmoji.gameObject.SetActive(false);
        hintIndicatorSendEmoji.gameObject.SetActive(true);
    }

    void sendMessage()
    {
        hintIndicatorSendEmoji.gameObject.SetActive(false);
        emojiLaughing.gameObject.SetActive(false);  
        firefly2.gameObject.SetActive(true);
        textMessageWithEmojiLaughing.gameObject.SetActive(true);
        toNextScenario.gameObject.SetActive(true);
        Color laughingColorInText = emojiLaughinginMessage.color;
        laughingColorInText.a = 1f;
        emojiLaughinginMessage.color = laughingColorInText;
        animator.SetTrigger("sendEmoji");
        animatorSendingMessage.SetTrigger("sendMessage");
    }


    void proceedToNextScenario()
    {
        Scenario2.gameObject.SetActive(true);
        Scenario1.gameObject.SetActive(false);
    }
}
    