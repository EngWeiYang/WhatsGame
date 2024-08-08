using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollBarListener : MonoBehaviour
{
    public Scrollbar scrollbar;

    public GameObject easyFront;
    public GameObject easyBack;
    public GameObject mediumFront;
    public GameObject mediumBack;
    public GameObject hardFront;
    public GameObject hardBack;

    public float easyThreshold;  // Threshold for easy
    public float mediumThreshold;  // Threshold for medium
    public float hardThreshold;  // Threshold for hard

    void Start()
    {
        if (scrollbar != null)
        {
            scrollbar.onValueChanged.AddListener(OnScrollBarValueChanged);
            easyFront.SetActive(true);
            easyBack.SetActive(true);
            mediumFront.SetActive(false);
            mediumBack.SetActive(false);
            hardFront.SetActive(false);
            hardBack.SetActive(false);
        }
    }

    void OnScrollBarValueChanged(float value)
    {
        Debug.Log(value);
        
        if (value <= easyThreshold && value >= mediumThreshold) 
        {
            easyFront.SetActive(true);
            easyBack.SetActive(true);
            mediumFront.SetActive(false);
            mediumBack.SetActive(false);
            hardFront.SetActive(false);
            hardBack.SetActive(false);
        }
        else if (value <= mediumThreshold && value >= hardThreshold)
        {
            easyFront.SetActive(false);
            easyBack.SetActive(false);
            mediumFront.SetActive(true);
            mediumBack.SetActive(true);
            hardFront.SetActive(false);
            hardBack.SetActive(false);
        }
        else if (value <= hardThreshold)
        {
            easyFront.SetActive(false);
            easyBack.SetActive(false);
            mediumFront.SetActive(false);
            mediumBack.SetActive(false);
            hardFront.SetActive(true);
            hardBack.SetActive(true);
        }
    }
}
