using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmojiClickedHeart : MonoBehaviour
{

    public Button emojiHeartBtn;
    public Image emojiHeart;
    public Image emojiHeartinMessage;
    public TMP_Text text;
    public GameObject sendMessageBtn;
    public GameObject textMessageWithEmojiHeart;
    public Button sendMessageText;
    public Animator animator;
    public GameObject defaultMessageBtn;
    public Button toNextScenario;
    public GameObject Scenario2;
    public GameObject Scenario3;
    public GameObject hintIndicatorEmoji;
    public GameObject hintIndicatorSendEmoji;


    void Start()
    {
        emojiHeartBtn.onClick.AddListener(clickedLaughingEmoji);
        sendMessageText.onClick.AddListener(sendMessage);
        toNextScenario.onClick.AddListener(proceedToNextScenario);
        Color heartEmojiColor = emojiHeart.color;
        heartEmojiColor.a = 0f;
        emojiHeart.color = heartEmojiColor;
    }

    void clickedLaughingEmoji()
    {
        Color heartEmojiColor = emojiHeart.color;
        heartEmojiColor.a = 1f;
        emojiHeart.color = heartEmojiColor;
        text.gameObject.SetActive(false);
        sendMessageBtn.gameObject.SetActive(true);
        defaultMessageBtn.gameObject.SetActive(false);
        hintIndicatorEmoji.gameObject.SetActive(false);
        hintIndicatorSendEmoji.gameObject.SetActive(true);
    }

    void sendMessage()
    {
        hintIndicatorSendEmoji.gameObject.SetActive(false);
        textMessageWithEmojiHeart.gameObject.SetActive(true);
        toNextScenario.gameObject.SetActive(true);
        Color heartEmojiColorInText = emojiHeartinMessage.color;
        heartEmojiColorInText.a = 1f;
        emojiHeartinMessage.color = heartEmojiColorInText;
        animator.SetTrigger("heartEmojiSend");
    }


    void proceedToNextScenario()
    {
        Scenario3.gameObject.SetActive(true);
        Scenario2.gameObject.SetActive(false);
    }
}
