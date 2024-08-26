using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

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

    private bool screenTooSmall = false;

    public float offset;
    public float offset2;

    public Transform mobileDetectorSquare;

    // Start is called before the first frame update
    void Start()
    {
        if (Platform.IsMobileBrowser())
        {
            mobileDetectorSquare.GetComponent<Image>().color = Color.green;
        }

        chatRT = chat.GetComponent<RectTransform>();
        inputBoxRT = inputBox.GetComponent<RectTransform>();

        chatCollider = chat.GetComponent<BoxCollider2D>();
        inputBoxCollider = inputBox.GetComponent<BoxCollider2D>();

        replyRT = reply.GetComponent<RectTransform>();
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

            chatRT.anchoredPosition = new Vector2(chatRT.anchoredPosition.x, inputBoxRT.anchoredPosition.y + offset);
        }
    }

    public void EmojiMoveUp()
    {
        if (screenTooSmall)
        {
            Debug.Log("H");

            replyRT.anchoredPosition = new Vector2(replyRT.anchoredPosition.x, inputBoxRT.anchoredPosition.y + offset2);

            chatRT.anchoredPosition = new Vector2(chatRT.anchoredPosition.x, inputBoxRT.anchoredPosition.y + offset - offset2);
        }
    }

    public void GIFMoveUp()
    {
        if (screenTooSmall)
        {
            Debug.Log("H");
        }
    }
}
