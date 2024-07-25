using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sendingGIFs : MonoBehaviour
{
    public Button sendGIFBtn;
    public GameObject sendGIFScreen;
    public GameObject chatScreen;
    public Animator sendGIFAnim;


    private void Start()
    {
        sendGIFBtn.onClick.AddListener(confirmSendGIF);
    }

    void confirmSendGIF()
    {
        sendGIFScreen.gameObject.SetActive(false);
        chatScreen.gameObject.SetActive(true);
        sendGIFAnim.SetTrigger("sendGIF");
    }
}
