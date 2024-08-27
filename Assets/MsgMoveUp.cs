using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;

public class MsgMoveUp : MonoBehaviour
{
    public GameObject chat;
    private RectTransform chatRT;
    private BoxCollider2D chatCollider;

    public GameObject inputBox;
    private RectTransform inputBoxRT;
    private BoxCollider2D inputBoxCollider;

    public GameObject reply;
    private RectTransform replyRT;

    public GameObject gifMsg;
    private RectTransform gifMsgRT;

    public GameObject gifReply;
    private RectTransform gifReplyRT;

    private bool screenTooSmall = false;

    public float LaughingEmojiOffset;
    public float LaughingEmojiOffset2;
    public float LaughingEmojiOffset3;
    public float gifReplyOffset;
    public float gifMsgOffset;

    public Transform mobileDetectorSquare;

    #region WebGL is on mobile check

    [DllImport("__Internal")]
    private static extern bool IsMobileBrowser();

    public bool isMobileBrowser()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
            return IsMobileBrowser();
#endif
        return false;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (isMobileBrowser() == true)
        {
            mobileDetectorSquare.GetComponent<Image>().color = Color.green;
        }
        else
        {
            mobileDetectorSquare.GetComponent<Image>().color = Color.cyan;
        }

        chatRT = chat.GetComponent<RectTransform>();
        inputBoxRT = inputBox.GetComponent<RectTransform>();

        chatCollider = chat.GetComponent<BoxCollider2D>();
        inputBoxCollider = inputBox.GetComponent<BoxCollider2D>();

        replyRT = reply.GetComponent<RectTransform>();

        gifReplyRT = gifReply.GetComponent<RectTransform>();

        gifMsgRT = gifMsg.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (chatCollider.IsTouching(inputBoxCollider))
        {
            Debug.Log("Touch");
            screenTooSmall = true;
        
            chatCollider.enabled = false;

            //Change Anchor
            chatRT.anchorMin = new Vector2(0, 0f);
            chatRT.anchorMax = new Vector2(0, 0f);

            chatRT.anchoredPosition = new Vector2(chatRT.anchoredPosition.x, inputBoxRT.anchoredPosition.y + LaughingEmojiOffset);
        }
    }

    public void EmojiMoveUp()
    {
        if (screenTooSmall)
        {
            //Change Anchor
            replyRT.anchorMin = new Vector2(1, 0f);
            replyRT.anchorMax = new Vector2(1, 0f);

            replyRT.anchoredPosition = new Vector2(replyRT.anchoredPosition.x, inputBoxRT.anchoredPosition.y + LaughingEmojiOffset2);

            chatRT.anchoredPosition = new Vector2(chatRT.anchoredPosition.x, inputBoxRT.anchoredPosition.y + LaughingEmojiOffset2 + LaughingEmojiOffset3);
        }
    }

    public void GIFMoveUp()
    {
        if (screenTooSmall)
        {
            //Change Anchors
            gifReplyRT.anchorMin = new Vector2(1, 0f);
            gifReplyRT.anchorMax = new Vector2(1, 0f);

            gifMsgRT.anchorMin = new Vector2(0, 0f);
            gifMsgRT.anchorMax = new Vector2(0, 0f);

            //Move gif reply above input box
            gifReplyRT.anchoredPosition = new Vector2(gifReplyRT.anchoredPosition.x, inputBoxRT.anchoredPosition.y + gifReplyOffset);

            //Move msg above reply
            gifMsgRT.anchoredPosition = new Vector2(gifMsgRT.anchoredPosition.x, inputBoxRT.anchoredPosition.y + gifReplyOffset + gifMsgOffset);
        }

        //Set transparency
        SetImageOpaque(gifReply.transform.Find("ChatBubble_Tail"));
        SetImageOpaque(gifReply.transform.Find("ChatBubble_Body"));
        SetImageOpaque(gifReply.transform.Find("ChatBubble_Body").Find("GoodMorning_GIF"));
        SetTextOpaque(gifReply.transform.Find("ChatBubble_Body").Find("Text_Time"));
    }

    public void StickerMoveUp()
    {
        if (screenTooSmall)
        {

        }
    }

    public void SetImageOpaque(Transform transform)
    {
        Image image = transform.GetComponent<Image>();
        Color imageColor;

        imageColor = image.color;
        imageColor.a = 1f;
        image.color = imageColor;
    }

    public void SetTextOpaque(Transform transform)
    {
        TMP_Text text = transform.GetComponent<TMP_Text>();
        Color imageColor;

        imageColor = text.color;
        imageColor.a = 1f;
        text.color = imageColor;
    }
}
