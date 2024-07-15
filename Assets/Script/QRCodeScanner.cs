using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QRCodeScanner : MonoBehaviour
{
    public GameObject Popup;
    public GameObject HintIndicator;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("QRCode"))
        {
            Popup.SetActive(true);
            HintIndicator.SetActive(true);
        }
    }
    
}
