using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollBarListener : MonoBehaviour
{
    // Reference to the UI Scrollbar
    public Scrollbar scrollbar;

    // GameObjects representing the different difficulty levels' front and back panels
    public GameObject easyFront;
    public GameObject easyBack;
    public GameObject mediumFront;
    public GameObject mediumBack;
    public GameObject hardFront;
    public GameObject hardBack;

    // Threshold values to determine when to switch between difficulty levels
    public float easyThreshold;   // Threshold for easy level
    public float mediumThreshold; // Threshold for medium level
    public float hardThreshold;   // Threshold for hard level

    void Start()
    {
        // Ensure the scrollbar is assigned
        if (scrollbar != null)
        {
            // Add a listener to the scrollbar that triggers when its value changes
            scrollbar.onValueChanged.AddListener(OnScrollBarValueChanged);

            // Initialize the UI: Start with 'easy' level panels active and others inactive
            easyFront.SetActive(true);
            easyBack.SetActive(true);
            mediumFront.SetActive(false);
            mediumBack.SetActive(false);
            hardFront.SetActive(false);
            hardBack.SetActive(false);
        }
    }

    // Method called whenever the scrollbar value changes
    void OnScrollBarValueChanged(float value)
    {
        // Log the scrollbar value to the console for debugging
        //Debug.Log(value);

        // Check which difficulty level range the scrollbar value falls into
        if (value <= easyThreshold && value >= mediumThreshold)
        {
            // Show 'easy' level panels and hide the others
            easyFront.SetActive(true);
            easyBack.SetActive(true);
            mediumFront.SetActive(false);
            mediumBack.SetActive(false);
            hardFront.SetActive(false);
            hardBack.SetActive(false);
        }
        else if (value <= mediumThreshold && value >= hardThreshold)
        {
            // Show 'medium' level panels and hide the others
            easyFront.SetActive(false);
            easyBack.SetActive(false);
            mediumFront.SetActive(true);
            mediumBack.SetActive(true);
            hardFront.SetActive(false);
            hardBack.SetActive(false);
        }
        else if (value <= hardThreshold)
        {
            // Show 'hard' level panels and hide the others
            easyFront.SetActive(false);
            easyBack.SetActive(false);
            mediumFront.SetActive(false);
            mediumBack.SetActive(false);
            hardFront.SetActive(true);
            hardBack.SetActive(true);
        }
    }
}
