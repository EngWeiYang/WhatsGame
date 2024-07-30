using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollBarListener : MonoBehaviour
{
    public Scrollbar scrollbar;
    public TextMeshProUGUI textToChange;
    public TextMeshProUGUI textShadowToChange;

    public Color easyColor;
    public Color mediumColor;
    public Color hardColor;
    public Color easyShadow;
    public Color mediumShadow;
    public Color hardShadow;

    public float easyThreshold;  // Threshold for easy
    public float mediumThreshold;  // Threshold for medium
    public float hardThreshold;  // Threshold for hard

    void Start()
    {
        if (scrollbar != null)
        {
            scrollbar.onValueChanged.AddListener(OnScrollBarValueChanged);
            ChangeFrontTextAndColor("Easy", easyColor);
            ChangeShadowTextAndColor("Easy", easyShadow);
        }
    }

    void OnScrollBarValueChanged(float value)
    {
        Debug.Log(value);
        
        if (value <= easyThreshold && value >= mediumThreshold) 
        {
            ChangeFrontTextAndColor("Easy", easyColor);
            ChangeShadowTextAndColor("Easy", easyShadow);
        }
        else if (value <= mediumThreshold && value >= hardThreshold)
        {
            ChangeFrontTextAndColor("Medium", mediumColor);
            ChangeShadowTextAndColor("Medium", mediumShadow);
        }
        else if (value <= hardThreshold)
        {
            ChangeFrontTextAndColor("Hard", hardColor);
            ChangeShadowTextAndColor("Hard", hardShadow);
        }
    }

    void ChangeFrontTextAndColor(string newText, Color newColor)
    {
        if (textToChange != null)
        {
            newColor.a = 1f;
            textToChange.text = newText;
            textToChange.color = newColor;
        }
    }

    void ChangeShadowTextAndColor(string newText, Color newColor)
    {
        if (textShadowToChange != null)
        {
            newColor.a = 1f; 
            textShadowToChange.text = newText;
            textShadowToChange.color = newColor;
        }
    }
}
