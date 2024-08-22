using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgMoveUp : MonoBehaviour
{
    private RectTransform chatRT;
    private BoxCollider2D chatCollider;

    public GameObject inputBox;
    private RectTransform inputBoxRT;
    private BoxCollider2D inputBoxCollider;

    public GameObject reply;
    private RectTransform replyRT;

    private bool screenTooSmall = false;

    public float constantVar;
    public float offset;

    // Start is called before the first frame update
    void Start()
    {
        chatRT = GetComponent<RectTransform>();
        inputBoxRT = inputBox.GetComponent<RectTransform>();

        chatCollider = GetComponent<BoxCollider2D>();
        inputBoxCollider = inputBox.GetComponent<BoxCollider2D>();

        replyRT = reply.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (chatCollider.IsTouching(inputBoxCollider))
        //{
        //    Debug.Log("Touch");
        //    screenTooSmall = true;
        //
        //    chatCollider.enabled = false;
        //
        //    chatRT.anchoredPosition = new Vector2(chatRT.anchoredPosition.x, inputBoxRT.anchoredPosition.y + 5/Screen.height);
        //}

        chatRT.anchoredPosition = new Vector2(chatRT.anchoredPosition.x, inputBoxRT.anchoredPosition.y + offset - (constantVar * Screen.height));
    }

    public void EmojiMoveUp()
    {
        if (screenTooSmall)
        {
            Debug.Log("H");

            replyRT.anchoredPosition = new Vector2(replyRT.anchoredPosition.x, inputBoxRT.anchoredPosition.y - 330);
        }
    }
}
