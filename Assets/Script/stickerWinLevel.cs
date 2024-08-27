using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class stickerWinLevel : MonoBehaviour
{
    public GameObject levelCompleteScreen;
    public Animator levelCompleteAnimator;
    public GameObject hintIndicator;
    public GameObject messageStickerReply;
    public GameObject firefly;
    public Button levelCompleteEnable;
    private CoroutineManager coroutineManager;

    public MsgMoveUp msgMoveUp;

    // Start is called before the first frame update
    private void Start()
    {
        // Subscribe to the button's onClick event
        levelCompleteEnable.onClick.AddListener(EnableLevelCompletion);
        GameObject coroutineManagerObject = GameObject.Find("CoroutineManager");
        coroutineManager = coroutineManagerObject.GetComponent<CoroutineManager>();
    }

    void EnableLevelCompletion()
    {
        msgMoveUp.StickerMoveUp();

        // Enable the level complete screen and trigger the animatio
        coroutineManager.StartManagedCoroutine(WinScreenCoroutine());
        hintIndicator.gameObject.SetActive(false);
        messageStickerReply.gameObject.SetActive(true);
        firefly.gameObject.SetActive(false);
    }
    IEnumerator WinScreenCoroutine()
    {
        // Wait for the animation to finish (assumes the animation length is 2 seconds)
        yield return new WaitForSeconds(1f);
        levelCompleteScreen.SetActive(true);
        levelCompleteAnimator.SetTrigger("LevelCompleted");

    }
}
