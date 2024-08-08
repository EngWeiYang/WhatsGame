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
    public GameObject hintIndicatorEmoji;
    public GameObject hintIndicatorEmojiClicked;
    public GameObject hintIndicatorSendEmoji;
    public GameObject firefly1;
    public GameObject firefly2;


    void Start()
    {
        emojiHeartBtn.onClick.AddListener(clickedLaughingEmoji);
        sendMessageText.onClick.AddListener(sendMessage);
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
        firefly1.gameObject.SetActive(false);
        firefly2.gameObject.SetActive(true);
        sendMessageBtn.gameObject.SetActive(true);
        defaultMessageBtn.gameObject.SetActive(false);
        hintIndicatorEmoji.gameObject.SetActive(false);
        hintIndicatorSendEmoji.gameObject.SetActive(true);
        hintIndicatorEmojiClicked.gameObject.SetActive(false);
    }

    void sendMessage()
    {
        hintIndicatorSendEmoji.gameObject.SetActive(false);
        textMessageWithEmojiHeart.gameObject.SetActive(true);
        toNextScenario.gameObject.SetActive(true);
        firefly2.gameObject.SetActive(true);
        emojiHeart.gameObject.SetActive (false);
        Color heartEmojiColorInText = emojiHeartinMessage.color;
        heartEmojiColorInText.a = 1f;
        emojiHeartinMessage.color = heartEmojiColorInText;
        animator.SetTrigger("heartEmojiSend");
    }
}
