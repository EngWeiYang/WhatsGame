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
    public GameObject hintIndicatorEmoji;
    public GameObject hintIndicatorEmojiClicked;
    public GameObject hintIndicatorSendEmoji;
    public GameObject fireflyInstructions;
    public GameObject fireflyScenario;
    public GameObject scenenario2;
    public GameObject scenenario3;
    private CoroutineManager coroutineManager;


    void Start()
    {
        emojiHeartBtn.onClick.AddListener(clickedLaughingEmoji);
        sendMessageText.onClick.AddListener(sendMessage);
        sendMessageText.onClick.AddListener(EnableScenarioFlow);
        GameObject coroutineManagerObject = GameObject.Find("CoroutineManager");
        coroutineManager = coroutineManagerObject.GetComponent<CoroutineManager>();
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
        hintIndicatorEmojiClicked.gameObject.SetActive(false);
    }

    void sendMessage()
    {
        hintIndicatorSendEmoji.gameObject.SetActive(false);
        textMessageWithEmojiHeart.gameObject.SetActive(true);
        emojiHeart.gameObject.SetActive (false);
        Color heartEmojiColorInText = emojiHeartinMessage.color;
        heartEmojiColorInText.a = 1f;
        emojiHeartinMessage.color = heartEmojiColorInText;
        animator.SetTrigger("heartEmojiSend");
    }

    void EnableScenarioFlow()
    {
        // Enable the level complete screen and trigger the animatio
        coroutineManager.StartManagedCoroutine(ScenarioFlow());
    }
    IEnumerator ScenarioFlow()
    {
        yield return new WaitForSeconds(1.5f);
        scenenario2.gameObject.SetActive(false);
        scenenario3.gameObject.SetActive(true);
        fireflyInstructions.gameObject.SetActive(false);
        fireflyScenario.gameObject.SetActive(true);


    }
}
