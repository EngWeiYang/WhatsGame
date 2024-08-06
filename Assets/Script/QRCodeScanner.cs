using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QRCodeScanner : MonoBehaviour
{
    public GameObject HintIndicator;
    public GameObject FireflyStep3;
    public GameObject FireflyStep4;
    public SlideAnim_NoSetInActive slideAnim; // Reference to the SlideAnim_NoSetInActive component
    public bool animtriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("QRCode"))
        {
            FireflyStep3.SetActive(false);
            FireflyStep4.SetActive(true);
            HintIndicator.SetActive(true);
            if(!animtriggered)
            {
                animtriggered = true;
                slideAnim.TriggerSlideAnimation();
                
            }
        }
    }
}
