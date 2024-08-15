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
    private CoroutineManager coroutineManager;


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
        sendGIFAnim.SetTrigger("sendGIF");
    }

    void EnableScenarioFlow()
    {
        // Enable the level complete screen and trigger the animatio
        coroutineManager.StartManagedCoroutine(ScenarioFlow());
    }
    IEnumerator ScenarioFlow()
    {
        yield return new WaitForSeconds(1f);
        // Wait for the animation to finish (assumes the animation length is 2 seconds)
        fireflyInstructions.gameObject.SetActive(false);
        fireflyScenario.gameObject.SetActive(true);
    }
}
