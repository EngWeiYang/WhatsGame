using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmojiClickedLaughing : MonoBehaviour
{
    public Button laughingEmojiBtn;
    public Image emojiLaughing;
    public Image emojiLaughinginMessage;
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
        defaultMessageBtn.gameObject.SetActive(false);
    }

    void sendMessage()
    {
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
    