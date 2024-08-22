using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using System;

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
    public Transform messageReplyEmojiLaugh;
    public Animator animatorSendingMessage;
    public GameObject defaultMessageBtn;
    public GameObject Scenario2;
    public GameObject Scenario1;
    public GameObject hintIndicatorEmoji;
    public GameObject hintIndicatorEmojiClick;
    public GameObject hintIndicatorSendEmoji;
    public GameObject fireflyInstructions;
    public GameObject fireflyScenario;
    public GameObject scenenario1;
    public GameObject scenenario2;
    private CoroutineManager coroutineManager;

    private RectTransform textMessageWithEmojiLaughingRT;
    public MsgMoveUp msgMoveUp;

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
        textMessageWithEmojiLaughingRT = textMessageWithEmojiLaughing.GetComponent<RectTransform>();
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

        //set transparency
        SetImageOpaque(messageReplyEmojiLaugh.Find("ChatBubble_Body"));
        SetImageOpaque(messageReplyEmojiLaugh.Find("ChatBubble_Anchor"));
        SetTextOpaque(messageReplyEmojiLaugh.Find("Text_Time"));
        SetTextOpaque(messageReplyEmojiLaugh.Find("EmojiLaugh"));

        msgMoveUp.EmojiMoveUp();

        //animator.SetTrigger("sendEmoji");
        //animatorSendingMessage.SetTrigger("sendMessage");
    }

    void EnableScenarioFlow()
    {
        // Enable the level complete screen and trigger the animatio
        coroutineManager.StartManagedCoroutine(ScenarioFlow());
    }
    IEnumerator ScenarioFlow()
    {
        yield return new WaitForSeconds(1.5f);
        scenenario1.gameObject.SetActive(false);
        scenenario2.gameObject.SetActive(true);
        fireflyInstructions.gameObject.SetActive(false);
        fireflyScenario.gameObject.SetActive(true);
        
    }

    private void SetImageOpaque(Transform transform)
    {
        Image image = transform.GetComponent<Image>();
        Color imageColor;

        imageColor = image.color;
        imageColor.a = 1f;
        image.color = imageColor;
    }

    private void SetTextOpaque(Transform transform)
    {
        TMP_Text text = transform.GetComponent<TMP_Text>();
        Color imageColor;

        imageColor = text.color;
        imageColor.a = 1f;
        text.color = imageColor;
    }
}
    