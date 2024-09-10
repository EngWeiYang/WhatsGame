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
    public GameObject lastlevelScreen;
    public Animator lastlevelAnimator;
    public Animator levelCompleteAnimator;
    public Animator levelCompleteCNAnimator;
    public Button sendButton;                // Assign in the Inspector
    public GameObject chatScreen;
    public GameObject cropScene;
    public GameObject camScreen;
    public Button lastlevelEnable;
    public Animator lastLevelCNAnimator;
    private CoroutineManager coroutineManager;
    public GameObject videoSent;

    private void Start()
    {
        sendButton.onClick.AddListener(OnSendButtonClick);
        lastlevelEnable.onClick.AddListener(EnableLevelCompletion);
        GameObject coroutineManagerObject = GameObject.Find("CoroutineManager");
        coroutineManager = coroutineManagerObject.GetComponent<CoroutineManager>();
    }

    public void OnSendButtonClick()
    {

        videoDurationTextTarget.text = cropRightScript.videoDuration.text;
        videoDurationTextTarget.text = cropperLeftScript.videoDuration.text;
        cropScene.gameObject.SetActive(false);
        camScreen.gameObject.SetActive(false);
        chatScreen.gameObject.SetActive(true);
        videoSent.gameObject.SetActive(true);
    }
    void EnableLevelCompletion()
    {
        // Enable the level complete screen and trigger the animatio
        coroutineManager.StartManagedCoroutine(WinScreenCoroutine());
    }
    IEnumerator WinScreenCoroutine()
    {
        // Wait for the animation to finish (assumes the animation length is 2 seconds)
        yield return new WaitForSeconds(1f);
        lastlevelScreen.SetActive(true);
        lastlevelAnimator.SetTrigger("LevelCompleted");
        lastLevelCNAnimator.SetTrigger("LevelCompleted");
        levelCompleteAnimator.SetTrigger("LevelCompleted");
        levelCompleteCNAnimator.SetTrigger("LevelCompleted");


    }
}
