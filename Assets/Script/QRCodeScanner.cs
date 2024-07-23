using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QRCodeScanner : MonoBehaviour
{
    public GameObject Popup;
    public GameObject HintIndicator;
    public GameObject FireflyStep3;
    public GameObject FireflyStep4;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("QRCode"))
        {
            Popup.SetActive(true);
            FireflyStep3.SetActive(false);
            FireflyStep4.SetActive(true);
            HintIndicator.SetActive(true);
        }
    }
    
}
