using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

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

    public GameObject stickerMsg;
    private RectTransform stickerMsgRT;

    public GameObject stickerReply;
    private RectTransform stickerReplyRT;

    public GameObject pollMsgs;
    private RectTransform pollMsgsRT;

    public GameObject pollReply;
    private RectTransform pollReplyRT;

    private bool screenTooSmall = false;

    public float LaughingEmojiOffset;
    public float LaughingEmojiOffset2;
    public float LaughingEmojiOffset3;
    public float gifReplyOffset;
    public float gifMsgOffset;

    public float stickerMsgOffset;
    public float stickerReplyOffset;

    public float pollMsgsOffset;
    public float pollReplyOffset;

    // Start is called before the first frame update
    void Start()
    {
        chatRT = chat.GetComponent<RectTransform>();
        inputBoxRT = inputBox.GetComponent<RectTransform>();

        chatCollider = chat.GetComponent<BoxCollider2D>();
        inputBoxCollider = inputBox.GetComponent<BoxCollider2D>();

        replyRT = reply.GetComponent<RectTransform>();

        gifMsgRT = gifMsg.GetComponent<RectTransform>();
        gifReplyRT = gifReply.GetComponent<RectTransform>();

        stickerMsgRT = stickerMsg.GetComponent<RectTransform>();
        stickerReplyRT = stickerReply.GetComponent<RectTransform>();

        pollMsgsRT = pollMsgs.GetComponent<RectTransform>();
        pollReplyRT = pollReply.GetComponent<RectTransform>();
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
            stickerMsgRT.anchorMin = new Vector2(0, 0f);
            stickerMsgRT.anchorMax = new Vector2(0, 0f);

            stickerReplyRT.anchorMin = new Vector2(1, 0f);
            stickerReplyRT.anchorMax = new Vector2(1, 0f);

            stickerReplyRT.anchoredPosition = new Vector2(stickerReplyRT.anchoredPosition.x, inputBoxRT.anchoredPosition.y + stickerReplyOffset);
            stickerMsgRT.anchoredPosition = new Vector2(stickerMsgRT.anchoredPosition.x, inputBoxRT.anchoredPosition.y + stickerReplyOffset + stickerMsgOffset);
        }
    }

    public void PollMoveUp()
    {
        if (screenTooSmall)
        {
            pollReplyRT.anchorMin = new Vector2(1, 0f);
            pollReplyRT.anchorMax = new Vector2(1, 0f);

            pollMsgsRT.anchorMin = new Vector2(0, 0f);
            pollMsgsRT.anchorMax = new Vector2(0, 0f);

            pollReplyRT.anchoredPosition = new Vector2(pollReplyRT.anchoredPosition.x, inputBoxRT.anchoredPosition.y + pollReplyOffset);

            pollMsgsRT.anchoredPosition = new Vector2(pollMsgsRT.anchoredPosition.x, inputBoxRT.anchoredPosition.y + pollMsgsOffset);
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
