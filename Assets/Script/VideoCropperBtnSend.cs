using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VideoCropperBtnSend : MonoBehaviour
{
    public TMP_Text videoDurationTextTarget; // Assign in the Inspector 
    public videoCropRight cropRightScript;   // Assign in the Inspector
    public VideoCropperLeft cropperLeftScript;
    public Button sendButton;                // Assign in the Inspector
    public GameObject chatScreen;
    public GameObject cropScene;
    public GameObject videoSent;

    private void Start()
    {
        sendButton.onClick.AddListener(OnSendButtonClick);
 
       
    }

    public void OnSendButtonClick()
    {

        videoDurationTextTarget.text = cropRightScript.videoDuration.text;
        videoDurationTextTarget.text = cropperLeftScript.videoDuration.text;
        cropScene.gameObject.SetActive(false);
        chatScreen.gameObject.SetActive(true);
        videoSent.gameObject.SetActive(true);
    }
}
