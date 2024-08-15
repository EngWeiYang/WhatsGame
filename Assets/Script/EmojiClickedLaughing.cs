using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

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
    public GameObject Scenario2;
    public GameObject Scenario1;
    public GameObject hintIndicatorEmoji;
    public GameObject hintIndicatorEmojiClick;
    public GameObject hintIndicatorSendEmoji;
    public GameObject fireflyInstructions;
    public GameObject fireflyScenario;
    private CoroutineManager coroutineManager;


    void Start()
    {
        laughingEmojiBtn.onClick.AddListener(clickedLaughingEmoji);
        sendMessageText.onClick.AddListener(sendMessage);
        sendMessageText.onClick.AddListener(EnableScenarioFlow);
        Color laughingColor = emojiLaughing.color;
        laughingColor.a = 0f;
        emojiLaughing.color = laughingColor;
        GameObject coroutineManagerObject = GameObject.Find("CoroutineManager");
        coroutineManager = coroutineManagerObject.GetComponent<CoroutineManager>();
    }

    void clickedLaughingEmoji()
    {
        Color laughingColor = emojiLaughing.color;
        laughingColor.a = 1f;
        emojiLaughing.color = laughingColor;
        text.gameObject.SetActive(false);
        sendMessageBtn.gameObject.SetActive(true);
        hintIndicatorEmojiClick.gameObject.SetActive(false);
        defaultMessageBtn.gameObject.SetActive(false);
        hintIndicatorEmoji.gameObject.SetActive(false);
        hintIndicatorSendEmoji.gameObject.SetActive(true);
    }

    void sendMessage()
    {
        hintIndicatorSendEmoji.gameObject.SetActive(false);
        emojiLaughing.gameObject.SetActive(false);  
        textMessageWithEmojiLaughing.gameObject.SetActive(true);
        Color laughingColorInText = emojiLaughinginMessage.color;
        laughingColorInText.a = 1f;
        emojiLaughinginMessage.color = laughingColorInText;
        animator.SetTrigger("sendEmoji");
        animatorSendingMessage.SetTrigger("sendMessage");
    }

    void EnableScenarioFlow()
    {
        // Enable the level complete screen and trigger the animatio
        coroutineManager.StartManagedCoroutine(ScenarioFlow());
    }
    IEnumerator ScenarioFlow()
    {
        yield return new WaitForSeconds(1f);
        // Wait for the animation to finish (assumes the animation length is 2 seconds)
        fireflyInstructions.gameObject.SetActive(false);
        fireflyScenario.gameObject.SetActive(true);
        

    }

}
    