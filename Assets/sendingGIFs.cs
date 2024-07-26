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
    public GameObject firefly1;
    public GameObject firefly2;
    public Button switchScenarioBtn;


    private void Start()
    {
        sendGIFBtn.onClick.AddListener(confirmSendGIF);
    }

    void confirmSendGIF()
    {
        sendGIFScreen.gameObject.SetActive(false);
        chatScreen.gameObject.SetActive(true);
        hintindicator.gameObject.SetActive(false);
        firefly1.gameObject.SetActive(false);
        firefly2.gameObject.SetActive(false);
        sendGIFAnim.SetTrigger("sendGIF");
        switchScenarioBtn.gameObject.SetActive(true);
    }
}
