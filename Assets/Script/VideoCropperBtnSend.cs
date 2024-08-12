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
    public GameObject levelCompleteScreen;
    public Animator levelCompleteAnimator;
    public Button levelCompleteEnable;
    private CoroutineManager coroutineManager;
    public GameObject videoSent;

    private void Start()
    {
        sendButton.onClick.AddListener(OnSendButtonClick);
        levelCompleteEnable.onClick.AddListener(WinScreenAfterInputsAreFilled);
        GameObject coroutineManagerObject = GameObject.Find("CoroutineManager");
        coroutineManager = coroutineManagerObject.GetComponent<CoroutineManager>();
    }

    public void OnSendButtonClick()
    {

        videoDurationTextTarget.text = cropRightScript.videoDuration.text;
        videoDurationTextTarget.text = cropperLeftScript.videoDuration.text;
        cropScene.gameObject.SetActive(false);
        chatScreen.gameObject.SetActive(true);
        videoSent.gameObject.SetActive(true);
    }

    void WinScreenAfterInputsAreFilled()
    {

      coroutineManager.StartManagedCoroutine(WinScreenCoroutine());
    }
    IEnumerator WinScreenCoroutine()
    {
        // Wait for the animation to finish (assumes the animation length is 2 seconds)
        yield return new WaitForSeconds(1f);
        levelCompleteScreen.SetActive(true);
        levelCompleteAnimator.SetTrigger("LevelCompleted");

    }
}
