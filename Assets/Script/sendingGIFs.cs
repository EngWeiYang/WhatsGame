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
    public GameObject hintindicator;
    public GameObject fireflyInstructions;
    public GameObject fireflyScenario;
    public GameObject scenenario3;
    public GameObject scenenario4;
    private CoroutineManager coroutineManager;

    public MsgMoveUp msgMoveUp;

    private void Start()
    {
        sendGIFBtn.onClick.AddListener(confirmSendGIF);
        sendGIFBtn.onClick.AddListener(EnableScenarioFlow);
        GameObject coroutineManagerObject = GameObject.Find("CoroutineManager");
        coroutineManager = coroutineManagerObject.GetComponent<CoroutineManager>();
    }

    void confirmSendGIF()
    {
        sendGIFScreen.gameObject.SetActive(false);
        chatScreen.gameObject.SetActive(true);
        hintindicator.gameObject.SetActive(false);

        msgMoveUp.GIFMoveUp();
    }

    void EnableScenarioFlow()
    {
        // Enable the level complete screen and trigger the animatio
        coroutineManager.StartManagedCoroutine(ScenarioFlow());
    }
    IEnumerator ScenarioFlow()
    {
        yield return new WaitForSeconds(1.5f);
        scenenario3.gameObject.SetActive(false);
        scenenario4.gameObject.SetActive(true);
        fireflyInstructions.gameObject.SetActive(false);
        fireflyScenario.gameObject.SetActive(true);
    }
}
